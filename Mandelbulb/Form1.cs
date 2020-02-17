using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

// Mandelbulb Explorer
// Copyright (c) Dmitry Brant, all rights reserved
//
// Use this code for any purpose, but please give credit where it's due.
// This code comes as-is, and with no warranty.

namespace Mandelbulb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = Application.ProductName;

            selectionCubeRadius = boxRadius / 2.0f;
            eyeOffset = new Vector3();
            eyeOffset.Z = -3.0f;

            boxOffset = new Vector3();
            selectionCubePos = new Vector3();

            plotPoints = new List<Point3f>();

            threads = new System.Threading.Thread[numThreads];
            threadFinished = new bool[numThreads];

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.ProductName + "\nCopyright Dmitry Brant\n\nhttps://dmitrybrant.com", "About...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        bool glLoaded = false;

        private void glControl1_Load(object sender, EventArgs e)
        {
            glLoaded = true;

            GL.ClearColor(Color.Black);

            glControl1_Resize(null, new EventArgs());
            btnDraw_Click(null, new EventArgs());
        }



        const int numThreads = 3;
        bool[] threadFinished;
        System.Threading.Thread[] threads;
        bool stopThreads = false;


        float angleX = 45.0f, angleY = 45.0f;
        Vector3 eyeOffset;
        int prevMouseX, prevMouseY;

        float boxRadius = 1.5f;
        Vector3 boxOffset;
        float pointsPerAxis = 100.0f;
        Vector3 selectionCubePos;
        float selectionCubeRadius;



        bool mouseDown = false;

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            prevMouseX = e.X;
            prevMouseY = e.Y;
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown) return;
            if (e.Button == MouseButtons.Left)
            {
                angleX += (e.X - prevMouseX);
                angleY -= (e.Y - prevMouseY);
                glControl1.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                eyeOffset.Z += ((e.X - prevMouseX) / 10.0f) * boxRadius;
                glControl1.Invalidate();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                eyeOffset.X += (float)((boxRadius / 100.0) * (e.X - prevMouseX));
                eyeOffset.Y -= (float)((boxRadius / 100.0) * (e.Y - prevMouseY));
                glControl1.Invalidate();
            }
            prevMouseX = e.X;
            prevMouseY = e.Y;
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!glLoaded) return;

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            glControl1.Invalidate();
        }




        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!glLoaded) return;

            double aspect = glControl1.Width / (double)glControl1.Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect, 0.0001f, boxRadius * 10.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);


            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


            if (chkShowFog.Checked)
            {
                float fogDensity = 0.3f;
                float fogStart = 0.0f;
                float fogEnd = boxRadius * 3;
                float[] fogColor = { 0.0f, 0.0f, 0.0f, 1.0f };

                GL.Enable(EnableCap.Fog);
                GL.Fog(FogParameter.FogColor, fogColor);
                GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
                GL.Fog(FogParameter.FogDensity, fogDensity);

                GL.Fog(FogParameter.FogStart, fogStart);
                GL.Fog(FogParameter.FogEnd, fogEnd);
                GL.Hint(HintTarget.FogHint, HintMode.Fastest);
            }
            else
            {
                GL.Disable(EnableCap.Fog);
            }



            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (chkAntialias.Checked)
            {
                GL.Enable(EnableCap.LineSmooth);
                GL.Enable(EnableCap.PointSmooth);
            }
            else
            {
                GL.Disable(EnableCap.LineSmooth);
                GL.Disable(EnableCap.PointSmooth);
            }

            GL.LineWidth(2.0f);
            GL.PointSize(Convert.ToSingle(udPointWidth.Value));


            Matrix4 lookat = Matrix4.LookAt(eyeOffset.X, eyeOffset.Y, eyeOffset.Z, 0, 0, 0, 0, 1, 0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Rotate(angleY, 1.0f, 0, 0);
            GL.Rotate(angleX, 0, 1.0f, 0);

            GL.Translate(-boxOffset);




            GL.Begin(BeginMode.Points);
            lock (plotPoints)
            {
                foreach (var p in plotPoints)
                {
                    //GL.FogCoord(p.z);// - boxOffset.Z);
                    GL.Color3(p.r, p.b, p.g);
                    GL.Vertex3(p.x, p.y, p.z);
                }
            }
            GL.End();


            if (chkShowAxes.Checked)
            {
                //draw axes...
                GL.Begin(BeginMode.Lines);
                GL.Color3(Color.Red);
                GL.Vertex3(-boxRadius + boxOffset.X, boxOffset.Y, boxOffset.Z); GL.Vertex3(boxRadius + boxOffset.X, boxOffset.Y, boxOffset.Z);
                GL.Color3(Color.Green);
                GL.Vertex3(boxOffset.X, -boxRadius + boxOffset.Y, boxOffset.Z); GL.Vertex3(boxOffset.X, boxRadius + boxOffset.Y, boxOffset.Z);
                GL.Color3(Color.Blue);
                GL.Vertex3(boxOffset.X, boxOffset.Y, -boxRadius + boxOffset.Z); GL.Vertex3(boxOffset.X, boxOffset.Y, boxRadius + boxOffset.Z);
                GL.End();
            }

            if (chkShowSelectionCube.Checked)
            {
                //draw selection cube...
                //GL.Enable(EnableCap.DepthTest);
                //GL.Enable(EnableCap.Blend);
                //GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                GL.Color4((byte)80, (byte)150, (byte)200, (byte)100);
                GL.Begin(BeginMode.Quads);
                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);

                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);

                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);

                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);

                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y - selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);

                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z - selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X + selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.Vertex3(selectionCubePos.X - selectionCubeRadius, selectionCubePos.Y + selectionCubeRadius, selectionCubePos.Z + selectionCubeRadius);
                GL.End();
            }



            glControl1.SwapBuffers();

        }



        private void StopThreads()
        {
            stopThreads = true;
            for (int i = 0; i < threads.Length; i++)
                if (threads[i] != null) { threads[i].Join(); threads[i] = null; }
        }


        private void btnDraw_Click(object sender, EventArgs e)
        {

            StopThreads();
            plotPoints.Clear();
            GC.Collect();

            stopThreads = false;
            Array.Clear(threadFinished, 0, numThreads);

            pointsPerAxis = Convert.ToSingle(udPointsPerAxis.Value);

            threads[0] = new System.Threading.Thread((System.Threading.ThreadStart)delegate { GeneratePoints(Convert.ToSingle(udPower.Value), Convert.ToInt32(udIterations.Value), 0, 0); });
            threads[1] = new System.Threading.Thread((System.Threading.ThreadStart)delegate { GeneratePoints(Convert.ToSingle(udPower.Value), Convert.ToInt32(udIterations.Value), 1, 1); });
            threads[2] = new System.Threading.Thread((System.Threading.ThreadStart)delegate { GeneratePoints(Convert.ToSingle(udPower.Value), Convert.ToInt32(udIterations.Value), 2, 2); });

            for (int i = 0; i < threads.Length; i++) { threads[i].Priority = System.Threading.ThreadPriority.BelowNormal; threads[i].Start(); }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopThreads();
        }





        List<Point3f> plotPoints;


        void GeneratePoints(float order, int maxIterations, int threadIndex, int whichWay)
        {
            List<Point3f> tempPoints = new List<Point3f>();

            float minX = -boxRadius + boxOffset.X, maxX = boxRadius + boxOffset.X;
            float minY = -boxRadius + boxOffset.Y, maxY = boxRadius + boxOffset.Y;
            float minZ = -boxRadius + boxOffset.Z, maxZ = boxRadius + boxOffset.Z;

            float pointIncrement = (boxRadius * 2.0f / pointsPerAxis);
            float incrX = pointIncrement, incrY = pointIncrement, incrZ = pointIncrement;

            float xsquare, ysquare, zsquare, bail = 10;
            float x, y, z, r, theta, phi, powr, sinThetaOrder;
            float tempx, tempy, tempz;
            int iteration;
            bool lastIn = false, firstPoint;

            if (whichWay == 0)
            {
                for (float z0 = minZ; z0 <= maxZ; z0 += incrZ)
                {
                    for (float x0 = minX; x0 <= maxX; x0 += incrX)
                    {
                        firstPoint = true;
                        for (float y0 = minY; y0 <= maxY; y0 += incrY)
                        {

                            x = 0; y = 0; z = 0;
                            iteration = 0;

                            while (true)
                            {
                                xsquare = x * x;
                                ysquare = y * y;
                                zsquare = z * z;

                                tempx = x; tempy = y; tempz = z;


                                r = (float)Math.Sqrt(xsquare + ysquare + zsquare);

                                theta = (float)Math.Atan2(Math.Sqrt(xsquare + ysquare), z);
                                phi = (float)Math.Atan2(y, x);
                                powr = (float)Math.Pow(r, order);

                                sinThetaOrder = (float)Math.Sin(theta * order);
                                x = (float)(powr * sinThetaOrder * Math.Cos(phi * order));
                                y = (float)(powr * sinThetaOrder * Math.Sin(phi * order));
                                z = (float)(powr * Math.Cos(theta * order));


                                x += x0; y += y0; z += z0;


                                iteration++;

                                if (iteration > maxIterations)
                                {
                                    //inside the set!
                                    if (!lastIn)
                                    {
                                        Point3f p = new Point3f();
                                        p.z = z0;
                                        p.x = x0;
                                        p.y = y0;
                                        if (!firstPoint) tempPoints.Add(p);
                                        lastIn = true;
                                    }
                                    firstPoint = false;
                                    break;
                                }
                                if ((xsquare + ysquare + zsquare) > bail)
                                {
                                    if (lastIn)
                                    {
                                        Point3f p = new Point3f();
                                        p.z = z0;
                                        p.x = x0;
                                        p.y = y0;
                                        if (!firstPoint) tempPoints.Add(p);
                                        lastIn = false;
                                    }
                                    firstPoint = false;
                                    break;
                                }
                            }
                        }
                    }

                    lock (plotPoints)
                    {
                        foreach (var p in tempPoints)
                            plotPoints.Add(p);
                        tempPoints.Clear();
                    }
                    this.BeginInvoke(new MethodInvoker(delegate { glControl1.Invalidate(); }));
                    if (stopThreads) break;
                }

            }
                /*
            else if (whichWay == 1)
            {
                for (float z0 = minZ - pointIncrement * 0.66f; z0 <= maxZ; z0 += incrZ)
                {
                    for (float y0 = minY - pointIncrement * 0.66f; y0 <= maxY; y0 += incrY)
                    {
                        firstPoint = true;
                        for (float x0 = minX - pointIncrement * 0.66f; x0 <= maxX; x0 += incrX)
                        {

                            x = 0; y = 0; z = 0;
                            iteration = 0;

                            while (true)
                            {
                                xsquare = x * x;
                                ysquare = y * y;
                                zsquare = z * z;

                                r = (float)Math.Sqrt(xsquare + ysquare + zsquare);

                                theta = (float)Math.Atan2(Math.Sqrt(xsquare + ysquare), z);
                                phi = (float)Math.Atan2(y, x);
                                powr = (float)Math.Pow(r, order);

                                sinThetaOrder = (float)Math.Sin(theta * order);
                                x = (float)(powr * sinThetaOrder * Math.Cos(phi * order));
                                y = (float)(powr * sinThetaOrder * Math.Sin(phi * order));
                                z = (float)(powr * Math.Cos(theta * order));

                                x += x0; y += y0; z += z0;

                                iteration++;

                                if (iteration > maxIterations)
                                {
                                    //inside the set!
                                    if (!lastIn)
                                    {
                                        Point3f p = new Point3f();
                                        p.z = z0;
                                        p.x = x0;
                                        p.y = y0;
                                        if (!firstPoint) tempPoints.Add(p);
                                        lastIn = true;
                                    }
                                    firstPoint = false;
                                    break;
                                }
                                if ((xsquare + ysquare + zsquare) > bail)
                                {
                                    if (lastIn)
                                    {
                                        Point3f p = new Point3f();
                                        p.z = z0;
                                        p.x = x0;
                                        p.y = y0;
                                        if (!firstPoint) tempPoints.Add(p);
                                        lastIn = false;
                                    }
                                    firstPoint = false;
                                    break;
                                }
                            }
                        }
                    }

                    lock (plotPoints)
                    {
                        foreach (var p in tempPoints)
                            plotPoints.Add(p);
                        tempPoints.Clear();
                    }
                    this.BeginInvoke(new MethodInvoker(delegate { glControl1.Invalidate(); }));
                    if (stopThreads) break;
                }
            }
            else if (whichWay == 2)
            {
                for (float x0 = minX - pointIncrement * 0.33f; x0 <= maxX; x0 += incrX)
                {
                    for (float y0 = minY - pointIncrement * 0.33f; y0 <= maxY; y0 += incrY)
                    {
                        firstPoint = true;
                        for (float z0 = minZ - pointIncrement * 0.33f; z0 <= maxZ; z0 += incrZ)
                        {

                            x = 0; y = 0; z = 0;
                            iteration = 0;

                            while (true)
                            {
                                xsquare = x * x;
                                ysquare = y * y;
                                zsquare = z * z;

                                r = (float)Math.Sqrt(xsquare + ysquare + zsquare);

                                theta = (float)Math.Atan2(Math.Sqrt(xsquare + ysquare), z);
                                phi = (float)Math.Atan2(y, x);
                                powr = (float)Math.Pow(r, order);

                                sinThetaOrder = (float)Math.Sin(theta * order);
                                x = (float)(powr * sinThetaOrder * Math.Cos(phi * order));
                                y = (float)(powr * sinThetaOrder * Math.Sin(phi * order));
                                z = (float)(powr * Math.Cos(theta * order));

                                x += x0; y += y0; z += z0;

                                iteration++;

                                if (iteration > maxIterations)
                                {
                                    //inside the set!
                                    if (!lastIn)
                                    {
                                        Point3f p = new Point3f();
                                        p.z = z0;
                                        p.x = x0;
                                        p.y = y0;
                                        if (!firstPoint) tempPoints.Add(p);
                                        lastIn = true;
                                    }
                                    firstPoint = false;
                                    break;
                                }
                                if ((xsquare + ysquare + zsquare) > bail)
                                {
                                    if (lastIn)
                                    {
                                        Point3f p = new Point3f();
                                        p.z = z0;
                                        p.x = x0;
                                        p.y = y0;
                                        if (!firstPoint) tempPoints.Add(p);
                                        lastIn = false;
                                    }
                                    firstPoint = false;
                                    break;
                                }
                            }

                        }
                    }

                    lock (plotPoints)
                    {
                        foreach (var p in tempPoints)
                            plotPoints.Add(p);
                        tempPoints.Clear();
                    }
                    this.BeginInvoke(new MethodInvoker(delegate { glControl1.Invalidate(); }));
                    if (stopThreads) break;
                }
            }
            */

            threadFinished[threadIndex] = true;
        }





        private class Point3f
        {
            public float x, y, z, distance;
            public byte r = 255, g = 255, b = 255;
        }

        private void tbCubeSize_Scroll(object sender, EventArgs e)
        {
            if (!glLoaded) return;
            selectionCubeRadius = (boxRadius / (float)tbCubeSize.Maximum) * (float)tbCubeSize.Value;

            selectionCubePos.X = (boxRadius * 2.0f / (float)tbCubeX.Maximum) * (float)tbCubeX.Value - boxRadius;
            selectionCubePos.Y = (boxRadius * 2.0f / (float)tbCubeY.Maximum) * (float)tbCubeY.Value - boxRadius;
            selectionCubePos.Z = (boxRadius * 2.0f / (float)tbCubeZ.Maximum) * (float)tbCubeZ.Value - boxRadius;

            selectionCubePos += boxOffset;

            glControl1.Invalidate();
        }

        private void chkShowSelectionCube_CheckedChanged(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void btnColorize_Click(object sender, EventArgs e)
        {
            //normalize colors...
            float minDistance = 10000.0f;
            float maxDistance = 0.0f;
            lock (plotPoints)
            {
                foreach (var p in plotPoints)
                {
                    p.distance = ((p.x - boxOffset.X) * (p.x - boxOffset.X) + (p.y - boxOffset.Y) * (p.y - boxOffset.Y) + (p.z - boxOffset.Z) * (p.z - boxOffset.Z));
                    if (p.distance < minDistance) minDistance = p.distance;
                    if (p.distance > maxDistance) maxDistance = p.distance;
                }

                float distanceRatio = maxDistance - minDistance;
                foreach (var p in plotPoints)
                {
                    p.distance -= minDistance;
                    p.distance /= distanceRatio;
                    p.distance *= 3.0f;

                    if (p.distance >= 2.0f)
                    {
                        p.r = (byte)((p.distance - 2.0f) * 255.0f);
                        p.g = (byte)(255 - p.r);
                        p.b = 0;
                    }
                    else if (p.distance >= 1.0f)
                    {
                        p.r = 0;
                        p.g = (byte)((p.distance - 1.0f) * 255.0f);
                        p.b = (byte)(255 - p.g);
                    }
                    else
                    {
                        p.b = (byte)(p.distance * 255.0f);
                        p.r = (byte)(255 - p.b);
                        p.g = 0;
                    }
                }
            }
            glControl1.Invalidate();
        }

        private void btnZoomCube_Click(object sender, EventArgs e)
        {
            boxOffset.X = selectionCubePos.X;
            boxOffset.Y = selectionCubePos.Y;
            boxOffset.Z = selectionCubePos.Z;

            eyeOffset.Z /= (boxRadius / selectionCubeRadius);
            boxRadius = selectionCubeRadius;

            tbCubeSize_Scroll(null, new EventArgs());
            btnDraw_Click(null, new EventArgs());
            glControl1.Invalidate();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            eyeOffset.Z *= 2;
            boxRadius *= 2;

            tbCubeSize_Scroll(null, new EventArgs());
            btnDraw_Click(null, new EventArgs());
            glControl1.Invalidate();
        }

        private void btnZoomOriginal_Click(object sender, EventArgs e)
        {
            eyeOffset.Z = -3.0f;
            boxRadius = 1.2f;

            boxOffset.X = selectionCubePos.X = 0.0f;
            boxOffset.Y = selectionCubePos.X = 0.0f;
            boxOffset.Z = selectionCubePos.X = 0.0f;
            selectionCubeRadius = boxRadius / 2.0f;

            tbCubeSize_Scroll(null, new EventArgs());
            btnDraw_Click(null, new EventArgs());
            glControl1.Invalidate();
        }





    }
}

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    internal class ShapeBatcher
    {
        public int ShapeCount { get; private set; }
        public int VertexCount { get; private set; }
        public int IndexCount { get; private set; }
        public bool IsStarted { get; private set; }

        private const int MAXVERTEX = 255;
        private GraphicsDevice graphicsDevice;
        private VertexPositionColor[] vertexList;
        private int[] indexList;
        private BasicEffect effect;

        public ShapeBatcher(GraphicsDevice pGraphicsDevice)
        {
            this.graphicsDevice = pGraphicsDevice;
            this.ShapeCount = 0;
            this.VertexCount = 0;
            this.IndexCount = 0;
            this.IsStarted = false;
            this.vertexList = new VertexPositionColor[MAXVERTEX];
            this.indexList = new int[MAXVERTEX * 3];

            this.effect = new BasicEffect(graphicsDevice);
            this.effect.FogEnabled = false;
            this.effect.TextureEnabled = false;
            this.effect.LightingEnabled = false;
            this.effect.VertexColorEnabled = true;
            this.effect.World = Matrix.Identity;
            this.effect.View = Matrix.Identity;
            this.effect.Projection = Matrix.Identity;
        }

        public void Begin()
        {
            if (IsStarted)
            {
                throw new Exception("The sprite batcher has allready been started");
            }

            Viewport viewport = graphicsDevice.Viewport;
            effect.Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, 0, viewport.Height, 0, 1);

            IsStarted = true;
        }

        private void Flush()
        {
            EnsureStarted();

            if(ShapeCount == 0)
            {
                return;
            }

            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    vertexList,
                    0,
                    VertexCount,
                    indexList,
                    0,
                    ShapeCount);
            }

            ShapeCount = 0;
            VertexCount = 0;
            IndexCount = 0;
        }

        public void End()
        {
            Flush();
            IsStarted = false;
        }

        private void EnsureStarted()
        {
            if (!IsStarted)
            {
                throw new Exception("The shape batcher has not yet been started");
            }
        }

        private void EnsureSpace(int pVertexAdded, int pIndexAdded)
        {
            if(VertexCount + pVertexAdded > MAXVERTEX || IndexCount + pIndexAdded > MAXVERTEX * 3)
            {
                Flush();
            }
        }

        public void DrawRectangle(Vector2 pPosition, bool pCentred, float pHeight, float pWidth, Color pColour)
        {
            EnsureStarted();
            EnsureSpace(4, 6);

            Vector3 vecA = new Vector3(pPosition, 0);
            Vector3 vecB = new Vector3(pPosition.X, pPosition.Y + pHeight, 0);
            Vector3 vecC = new Vector3(pPosition.X + pWidth, pPosition.Y + pHeight, 0);
            Vector3 vecD = new Vector3(pPosition.X + pWidth, pPosition.Y, 0);

            if (pCentred)
            {
                vecA -= new Vector3(pWidth / 2, pHeight / 2, 0);
                vecB -= new Vector3(pWidth / 2, pHeight / 2, 0);
                vecC -= new Vector3(pWidth / 2, pHeight / 2, 0);
                vecD -= new Vector3(pWidth / 2, pHeight / 2, 0);
            }

            VertexPositionColor a = new VertexPositionColor(vecA, pColour);
            VertexPositionColor b = new VertexPositionColor(vecB, pColour);
            VertexPositionColor c = new VertexPositionColor(vecC, pColour);
            VertexPositionColor d = new VertexPositionColor(vecD, pColour);

            indexList[IndexCount++] = 0 + VertexCount;
            indexList[IndexCount++] = 1 + VertexCount;
            indexList[IndexCount++] = 2 + VertexCount;
            indexList[IndexCount++] = 0 + VertexCount;
            indexList[IndexCount++] = 2 + VertexCount;
            indexList[IndexCount++] = 3 + VertexCount;

            vertexList[VertexCount++] = a;
            vertexList[VertexCount++] = b;
            vertexList[VertexCount++] = c;
            vertexList[VertexCount++] = d;

            ShapeCount += 2;
        }

        public void DrawCircle(Vector2 pPosition, float pRadius, int pSides, Color pColour)
        {
            EnsureStarted();
            EnsureSpace(1 + 2 * pSides, 3 * pSides);

            float deltaAngle = MathF.PI * 2 / pSides;
            float angle = 0;

            int centerIndex = VertexCount++;
            vertexList[centerIndex] = new VertexPositionColor(new Vector3(pPosition, 0), pColour);

            for (int i = 0; i < pSides; i++)
            {
                VertexPositionColor a = new VertexPositionColor(new Vector3(pPosition.X + pRadius * MathF.Sin(angle), pPosition.Y + pRadius * MathF.Cos(angle), 0), pColour);
                angle += deltaAngle;
                VertexPositionColor b = new VertexPositionColor(new Vector3(pPosition.X + pRadius * MathF.Sin(angle), pPosition.Y + pRadius * MathF.Cos(angle), 0), pColour);

                indexList[IndexCount++] = centerIndex;
                indexList[IndexCount++] = 1 + VertexCount - 1;
                indexList[IndexCount++] = 2 + VertexCount - 1;

                vertexList[VertexCount++] = a;
                vertexList[VertexCount++] = b;
            }

            ShapeCount += pSides;
        }

        //Function doesnt work
        public void DrawEquTriangle(Vector2 pPosition, float pSize, float pRotation, Color pColour)
        {
            EnsureStarted();
            EnsureSpace(3, 3);

            Vector2 aVec = Vector2.Zero;
            Vector2 bVec = new Vector2(pSize, 0);
            Vector2 cVec = new Vector2(pSize / 2f, MathF.Sqrt(MathF.Pow(pSize, 2) - MathF.Pow(pSize / 2f, 2)));

            bVec = bVec.Rotate(pRotation);
            cVec = cVec.Rotate(pRotation);

            VertexPositionColor a = new VertexPositionColor(new Vector3(aVec + pPosition, 0), pColour);
            VertexPositionColor b = new VertexPositionColor(new Vector3(bVec + pPosition, 0), pColour);
            VertexPositionColor c = new VertexPositionColor(new Vector3(cVec + pPosition, 0), pColour);

            indexList[IndexCount++] = 0 + VertexCount;
            indexList[IndexCount++] = 1 + VertexCount;
            indexList[IndexCount++] = 2 + VertexCount;

            vertexList[VertexCount++] = a;
            vertexList[VertexCount++] = b;
            vertexList[VertexCount++] = c;

            ShapeCount++;
        }
    }
}

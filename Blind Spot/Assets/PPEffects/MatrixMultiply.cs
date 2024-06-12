using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessing
{
    [Serializable]
    [PostProcess(typeof(MatrixMultiplyRenderer), PostProcessEvent.AfterStack, "Custom/Matrix Multiply")]
    public sealed class MatrixMultiply : PostProcessEffectSettings
    {
        [Tooltip("Vector corresponding to the 1st column of the matrix.")]
        public Vector3Parameter XAxis = new() { value = Vector3.right };
        [Tooltip("Vector corresponding to the 2nd column of the matrix.")]
        public Vector3Parameter YAxis = new() { value = Vector3.up };
        [Tooltip("Vector corresponding to the 3rd column of the matrix.")]
        public Vector3Parameter ZAxis = new() { value = Vector3.forward };

        public (Vector3, Vector3, Vector3) Matrix
        {
            get => (XAxis.value, YAxis.value, ZAxis.value);
            set => (XAxis.value, YAxis.value, ZAxis.value) = (value.Item1, value.Item2, value.Item3);
        }
    }

    public sealed class MatrixMultiplyRenderer : PostProcessEffectRenderer<MatrixMultiply>
    {
        private static readonly int ShaderMatrix = Shader.PropertyToID("_ShaderMatrix");
/*
'Protanopia':   
    [0.567,0.433,0.000,
    0.558,0.442,0.000,
    0.000,0.242,0.758],
'Deuteranopia': 
    [0.625,0.375,0.000,
    0.700,0.300,0.000,
    0.000,0.300,0.700],
*/
        private Matrix4x4 GenerateMatrix()
        {
            Vector4 ToXYZ0(Vector3 vec)
            {
                return new Vector4(vec.x, vec.y, vec.z, 0);
            }

            return new Matrix4x4(
                ToXYZ0(settings.XAxis), 
                ToXYZ0(settings.YAxis), 
                ToXYZ0(settings.ZAxis),
                new Vector4(0, 0, 0, 1));
        }
        
        public override void Render(PostProcessRenderContext context)
        {
            var shader = context.propertySheets.Get(Shader.Find("Hidden/Custom/MatrixMultiply"));
            var matrix = GenerateMatrix();
            shader.properties.SetMatrix(ShaderMatrix, matrix);
            
            context.command.BlitFullscreenTriangle(context.source, context.destination, shader, 0);
        }
    }
}
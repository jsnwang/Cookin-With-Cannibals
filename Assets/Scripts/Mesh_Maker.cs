﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_Maker
{

    // Mesh Values
    private List<Vector3> _vertices = new List<Vector3>();
    private List<Vector3> _normals = new List<Vector3>();
    private List<Vector2> _uvs = new List<Vector2>();
    private List<Vector4> _tangents = new List<Vector4>();
    private List<List<int>> _subIndices = new List<List<int>>();

    public int VertCount
    {
        get
        {
            return _vertices.Count;
        }
    }

    /// <summary>
    /// Clears all arrays
    /// </summary>
    public void Clear()
    {
        _vertices.Clear();
        _normals.Clear();
        _uvs.Clear();
        _tangents.Clear();
        _subIndices.Clear();
    }

    public void AddTriangle(Triangle triangle, int submesh)
    {
        AddTriangle(triangle.vertices, triangle.uvs, triangle.normals, triangle.tangents, submesh);
    }

    /// <summary>
    /// Adds a new triangle to the return of GetMesh()
    /// </summary>
    /// <param name="vertices">Array of 3</param>
    /// <param name="normals">Array of 3</param>
    /// <param name="uvs">Array of 3</param>
    /// <param name="submesh">If you don't know put 0</param>
    public void AddTriangle(Vector3[] vertices, Vector2[] uvs, Vector3[] normals, int submesh = 0)
    {
        AddTriangle(vertices, uvs, normals, null, submesh);
    }
    /// <summary>
    /// Same as the first, but with tangents
    /// </summary>
    /// <param name="vertices">Array of 3</param>
    /// <param name="normals">Array of 3</param>
    /// <param name="uvs">Array of 3</param>
    /// <param name="tangents">Array of 3</param>
    /// <param name="submesh">If you don't know put 0</param>
    public void AddTriangle(Vector3[] vertices, Vector2[] uvs, Vector3[] normals, Vector4[] tangents, int submesh = 0)
    {
        int vertCount = _vertices.Count;

        _vertices.Add(vertices[0]);
        _vertices.Add(vertices[1]);
        _vertices.Add(vertices[2]);

        _normals.Add(normals[0]);
        _normals.Add(normals[1]);
        _normals.Add(normals[2]);

        _uvs.Add(uvs[0]);
        _uvs.Add(uvs[1]);
        _uvs.Add(uvs[2]);

        if (tangents != null)
        {
            _tangents.Add(tangents[0]);
            _tangents.Add(tangents[1]);
            _tangents.Add(tangents[2]);
        }

        if (_subIndices.Count < submesh + 1)
        {
            for (int i = _subIndices.Count; i < submesh + 1; i++)
            {
                _subIndices.Add(new List<int>());
            }
        }

        _subIndices[submesh].Add(vertCount);
        _subIndices[submesh].Add(vertCount + 1);
        _subIndices[submesh].Add(vertCount + 2);

    }

    /// <summary>
    /// Cleans up Double Vertices
    /// </summary>
    public void RemoveDoubles()
    {

        int dubCount = 0;

        Vector3 vertex = Vector3.zero;
        Vector3 normal = Vector3.zero;
        Vector2 uv = Vector2.zero;
        Vector4 tangent = Vector4.zero;

        int iterator = 0;
        while (iterator < VertCount)
        {

            vertex = _vertices[iterator];
            normal = _normals[iterator];
            uv = _uvs[iterator];

            // look backwards for a match
            for (int backward_iterator = iterator - 1; backward_iterator >= 0; backward_iterator--)
            {

                if (vertex == _vertices[backward_iterator] &&
                    normal == _normals[backward_iterator] &&
                    uv == _uvs[backward_iterator])
                {
                    dubCount++;
                    DoubleFound(backward_iterator, iterator);
                    iterator--;
                    break; // there should only be one
                }
            }

            iterator++;

        } // while

        Debug.LogFormat("Doubles found {0}", dubCount);

    }
    /// <summary>
    /// // go through all indices an replace them
    /// </summary>
    /// <param name="first"></param>
    /// <param name="duplicate"></param>
    private void DoubleFound(int first, int duplicate)
    {
        for (int h = 0; h < _subIndices.Count; h++)
        {
            for (int i = 0; i < _subIndices[h].Count; i++)
            {

                if (_subIndices[h][i] > duplicate) // knock it down
                    _subIndices[h][i]--;
                else if (_subIndices[h][i] == duplicate) // replace
                    _subIndices[h][i] = first;
            }
        }

        _vertices.RemoveAt(duplicate);
        _normals.RemoveAt(duplicate);
        _uvs.RemoveAt(duplicate);

        if (_tangents.Count > 0)
            _tangents.RemoveAt(duplicate);

    }

    /// <summary>
    /// Creates and returns a new mesh
    /// </summary>
    public Mesh GetMesh()
    {

        Mesh shape = new Mesh();
        shape.name = "Generated Mesh";
        shape.SetVertices(_vertices);
        shape.SetNormals(_normals);
        shape.SetUVs(0, _uvs);
        shape.SetUVs(1, _uvs);

        if (_tangents.Count > 1)
            shape.SetTangents(_tangents);

        shape.subMeshCount = _subIndices.Count;

        for (int i = 0; i < _subIndices.Count; i++)
            shape.SetTriangles(_subIndices[i], i);

        return shape;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Creates and returns a new mesh with generated lightmap uvs (Editor Only)
    /// </summary>
    public Mesh GetMesh_GenerateSecondaryUVSet()
    {

        Mesh shape = GetMesh();

        // for light mapping
        UnityEditor.Unwrapping.GenerateSecondaryUVSet(shape);

        return shape;
    }

    /// <summary>
    /// Creates and returns a new mesh with generated lightmap uvs (Editor Only)
    /// </summary>
    public Mesh GetMesh_GenerateSecondaryUVSet(UnityEditor.UnwrapParam param)
    {

        Mesh shape = GetMesh();

        // for light mapping
        UnityEditor.Unwrapping.GenerateSecondaryUVSet(shape, param);

        return shape;
    }
#endif

    /// <summary>
    /// every property should have 3 elements
    /// </summary>
    public struct Triangle
    {
        public Vector3[] vertices;
        public Vector2[] uvs;
        public Vector3[] normals;
        public Vector4[] tangents;

        public Triangle(Vector3[] vertices = null, Vector2[] uvs = null, Vector3[] normals = null, Vector4[] tangents = null)
        {
            this.vertices = vertices;
            this.uvs = uvs;
            this.normals = normals;
            this.tangents = tangents;
        }
    }

}

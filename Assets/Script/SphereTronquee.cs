using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruncatedSphere : MonoBehaviour
{
    public int meridiens = 20;       // Nombre de m�ridiens autour de la sph�re (divisions horizontales)
    public int paralleles = 10;      // Nombre de parall�les (divisions verticales)
    public float rayon = 1f;         // Rayon de la sph�re
    public float angleTronque = 45f; // Angle � tronquer en degr�s (pour cr�er l'ouverture)

    void Start()
    {
        // Ajout des composants n�cessaires pour afficher la sph�re
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));      // Cr�ation des donn�es pour la sph�re tronqu�e
        CreateTruncatedSphere(mesh);
    }

    void CreateTruncatedSphere(Mesh mesh)
    {
        // Convertir l'angle � tronquer en radians
        float angleTronqueRad = Mathf.Deg2Rad * angleTronque;

        // Liste des sommets et triangles
        Vector3[] vertices = new Vector3[(meridiens + 1) * (paralleles + 1)];
        int[] triangles = new int[meridiens * paralleles * 6];

        // G�n�rer les sommets
        for (int i = 0; i <= paralleles; i++)
        {
            // Calcul de l'angle en latitude (parall�les)
            float theta = Mathf.PI * i / paralleles;
            for (int j = 0; j <= meridiens; j++)
            {
                // Calcul de l'angle en longitude (m�ridiens)
                float phi = 2 * Mathf.PI * j / meridiens;

                // Si le point est � l'int�rieur de l'angle tronqu�, ne pas le cr�er
                if (phi <= angleTronqueRad || phi >= (2 * Mathf.PI - angleTronqueRad))
                    continue;

                // Calcul des coordonn�es des sommets
                float x = rayon * Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = rayon * Mathf.Cos(theta);
                float z = rayon * Mathf.Sin(theta) * Mathf.Sin(phi);

                vertices[i * (meridiens + 1) + j] = new Vector3(x, y, z);
            }
        }

        // G�n�rer les triangles
        int triIndex = 0;
        for (int i = 0; i < paralleles; i++)
        {
            for (int j = 0; j < meridiens; j++)
            {
                if ((j * 2 * Mathf.PI / meridiens) <= angleTronqueRad || (j * 2 * Mathf.PI / meridiens) >= (2 * Mathf.PI - angleTronqueRad))
                    continue;

                int current = i * (meridiens + 1) + j;
                int next = current + meridiens + 1;

                // Triangle 1
                triangles[triIndex] = current;
                triangles[triIndex + 1] = next;
                triangles[triIndex + 2] = current + 1;

                // Triangle 2
                triangles[triIndex + 3] = current + 1;
                triangles[triIndex + 4] = next;
                triangles[triIndex + 5] = next + 1;

                triIndex += 6;
            }
        }

        // Appliquer les donn�es au mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
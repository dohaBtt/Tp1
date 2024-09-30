using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruncatedSphere : MonoBehaviour
{
    public int meridiens = 20;       // Nombre de méridiens autour de la sphère (divisions horizontales)
    public int paralleles = 10;      // Nombre de parallèles (divisions verticales)
    public float rayon = 1f;         // Rayon de la sphère
    public float angleTronque = 45f; // Angle à tronquer en degrés (pour créer l'ouverture)

    void Start()
    {
        // Ajout des composants nécessaires pour afficher la sphère
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));      // Création des données pour la sphère tronquée
        CreateTruncatedSphere(mesh);
    }

    void CreateTruncatedSphere(Mesh mesh)
    {
        // Convertir l'angle à tronquer en radians
        float angleTronqueRad = Mathf.Deg2Rad * angleTronque;

        // Liste des sommets et triangles
        Vector3[] vertices = new Vector3[(meridiens + 1) * (paralleles + 1)];
        int[] triangles = new int[meridiens * paralleles * 6];

        // Générer les sommets
        for (int i = 0; i <= paralleles; i++)
        {
            // Calcul de l'angle en latitude (parallèles)
            float theta = Mathf.PI * i / paralleles;
            for (int j = 0; j <= meridiens; j++)
            {
                // Calcul de l'angle en longitude (méridiens)
                float phi = 2 * Mathf.PI * j / meridiens;

                // Si le point est à l'intérieur de l'angle tronqué, ne pas le créer
                if (phi <= angleTronqueRad || phi >= (2 * Mathf.PI - angleTronqueRad))
                    continue;

                // Calcul des coordonnées des sommets
                float x = rayon * Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = rayon * Mathf.Cos(theta);
                float z = rayon * Mathf.Sin(theta) * Mathf.Sin(phi);

                vertices[i * (meridiens + 1) + j] = new Vector3(x, y, z);
            }
        }

        // Générer les triangles
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

        // Appliquer les données au mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
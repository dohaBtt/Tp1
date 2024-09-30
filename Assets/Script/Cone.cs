using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Cone : MonoBehaviour
{
    public float rayonBase = 1f;     // Rayon � la base du c�ne
    public float hauteur = 2f;       // Hauteur totale du c�ne
    public int nbMeridiens = 20;     // Nombre de m�ridiens (segments circulaires)

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

        CreerCone();
        MettreAJourMesh();
    }

    void CreerCone()
    {
        // Nombre de vertices (un cercle � la base + 1 sommet)
        int nbVertices = nbMeridiens + 2; // Sommets pour le cercle de la base + 1 pour le centre + 1 pour le sommet
        vertices = new Vector3[nbVertices];

        // Sommets du cercle de la base
        float angleStep = 360f / nbMeridiens;
        for (int i = 0; i <= nbMeridiens; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            float x = Mathf.Cos(angle) * rayonBase;
            float z = Mathf.Sin(angle) * rayonBase;
            vertices[i] = new Vector3(x, 0, z);  // Sommets du cercle de la base
        }

        // Ajout du sommet pointu
        vertices[vertices.Length - 2] = new Vector3(0, 0, 0);          // Centre de la base
        vertices[vertices.Length - 1] = new Vector3(0, hauteur, 0);    // Sommet pointu

        // Cr�ation des triangles
        triangles = new int[nbMeridiens * 6]; // 3 triangles par m�ridien (base + c�t�s)
        int tris = 0;

        // Triangles pour le couvercle de la base
        for (int i = 0; i < nbMeridiens; i++)
        {
            triangles[tris + 0] = i;
            triangles[tris + 1] = (i + 1) % nbMeridiens;
            triangles[tris + 2] = vertices.Length - 2; // Centre de la base

            tris += 3;
        }

        // Triangles pour les c�t�s du c�ne
        for (int i = 0; i < nbMeridiens; i++)
        {
            triangles[tris + 0] = i;
            triangles[tris + 1] = (i + 1) % nbMeridiens;
            triangles[tris + 2] = vertices.Length - 1; // Sommet pointu

            tris += 3;
        }
    }

    void MettreAJourMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}

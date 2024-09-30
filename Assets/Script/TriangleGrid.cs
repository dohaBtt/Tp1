using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TriangleGrid : MonoBehaviour
{
    public int nbLignes = 5;
    public int nbColonnes = 5;
    public float taille = 1f;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        // Applique un matériau standard à l'objet
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard")); // La ligne demandée
        CreerGrilleDeTriangles();
        MettreAJourMesh();
    }

    void CreerGrilleDeTriangles()
    {
        int numTriangles = nbLignes * nbColonnes * 2;
        vertices = new Vector3[(nbColonnes + 1) * (nbLignes + 1)];
        triangles = new int[numTriangles * 3]; // 3 sommets par triangle

        // Générer les vertices (sommets)
        for (int i = 0, z = 0; z <= nbLignes; z++)
        {
            for (int x = 0; x <= nbColonnes; x++)
            {
                vertices[i] = new Vector3(x * taille, 0, z * taille);
                i++;
            }
        }

        // Générer les triangles
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < nbLignes; z++)
        {
            for (int x = 0; x < nbColonnes; x++)
            {
                // Premier triangle (en haut à gauche)
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + nbColonnes + 1;
                triangles[tris + 2] = vert + 1;

                // Deuxième triangle (en bas à droite)
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + nbColonnes + 1;
                triangles[tris + 5] = vert + nbColonnes + 2;

                vert++;
                tris += 6;
            }
            vert++;
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

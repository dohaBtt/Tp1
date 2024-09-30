using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Cylindre : MonoBehaviour
{
    public float rayon = 1f;
    public float hauteur = 2f;
    public int nbMeridiens = 20;

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
        CreerCylindre();
        MettreAJourMesh();
    }

    void CreerCylindre()
    {
        int nbVertices = (nbMeridiens + 1) * 2 + 2;  // Sommets pour le corps + 2 centres pour les couvercles
        vertices = new Vector3[nbVertices];
        triangles = new int[nbMeridiens * 12];  // Chaque méridien génère 4 triangles (2 pour les couvercles, 2 pour les côtés)

        float angleStep = 360f / nbMeridiens;

        // Générer les vertices pour le corps du cylindre
        for (int i = 0; i <= nbMeridiens; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            float x = Mathf.Cos(angle) * rayon;
            float z = Mathf.Sin(angle) * rayon;

            vertices[i] = new Vector3(x, 0, z); // Sommets du bas
            vertices[i + nbMeridiens + 1] = new Vector3(x, hauteur, z); // Sommets du haut
        }

        // Sommets pour les centres des couvercles (en haut et en bas)
        vertices[vertices.Length - 2] = new Vector3(0, 0, 0); // Centre du couvercle bas
        vertices[vertices.Length - 1] = new Vector3(0, hauteur, 0); // Centre du couvercle haut

        // Générer les triangles pour les couvercles
        int tris = 0;
        for (int i = 0; i < nbMeridiens; i++)
        {
            // Couvercle bas
            triangles[tris + 0] = i;
            triangles[tris + 1] = (i + 1) % nbMeridiens;
            triangles[tris + 2] = vertices.Length - 2;

            // Couvercle haut
            triangles[tris + 3] = (i + 1) % nbMeridiens + nbMeridiens + 1;
            triangles[tris + 4] = i + nbMeridiens + 1;
            triangles[tris + 5] = vertices.Length - 1;

            tris += 6;
        }

        // Générer les triangles pour le corps du cylindre
        for (int i = 0; i < nbMeridiens; i++)
        {
            // Triangle 1
            triangles[tris + 0] = i;
            triangles[tris + 1] = i + nbMeridiens + 1;
            triangles[tris + 2] = (i + 1) % nbMeridiens;

            // Triangle 2
            triangles[tris + 3] = (i + 1) % nbMeridiens;
            triangles[tris + 4] = i + nbMeridiens + 1;
            triangles[tris + 5] = (i + 1) % nbMeridiens + nbMeridiens + 1;

            tris += 6;
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

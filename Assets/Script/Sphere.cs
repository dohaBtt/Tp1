using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Sphere : MonoBehaviour
{
    public float rayon = 1f;
    public int nbParalleles = 10;
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
        CreerSphere();
        MettreAJourMesh();
    }

    void CreerSphere()
    {
        // Calculer le nombre de sommets
        int nbVertices = (nbMeridiens + 1) * (nbParalleles - 1) + 2; // 2 sommets pour les pôles
        vertices = new Vector3[nbVertices];

        // Sommet du pôle nord
        vertices[0] = new Vector3(0, rayon, 0);

        // Générer les sommets pour les parallèles
        float phiStep = Mathf.PI / nbParalleles;
        float thetaStep = 2 * Mathf.PI / nbMeridiens;

        for (int i = 1, index = 1; i < nbParalleles; i++)
        {
            float phi = i * phiStep;
            float y = Mathf.Cos(phi) * rayon;
            float r = Mathf.Sin(phi) * rayon;

            for (int j = 0; j <= nbMeridiens; j++, index++)
            {
                float theta = j * thetaStep;
                float x = r * Mathf.Cos(theta);
                float z = r * Mathf.Sin(theta);
                vertices[index] = new Vector3(x, y, z);
            }
        }

        // Sommet du pôle sud
        vertices[vertices.Length - 1] = new Vector3(0, -rayon, 0);

        // Générer les triangles
        triangles = new int[nbMeridiens * 6 * (nbParalleles - 1)]; // Triangles pour chaque bande
        int tris = 0;

        // Connecter les pôles aux premiers/derniers méridiens
        for (int i = 0; i < nbMeridiens; i++)
        {
            // Triangles avec le pôle nord
            triangles[tris + 0] = 0;
            triangles[tris + 1] = i + 1;
            triangles[tris + 2] = (i + 1) % nbMeridiens + 1;

            tris += 3;
        }

        // Triangles des bandes entre les parallèles
        for (int i = 0; i < nbParalleles - 2; i++)
        {
            for (int j = 0; j < nbMeridiens; j++)
            {
                int current = i * (nbMeridiens + 1) + j + 1;
                int next = current + nbMeridiens + 1;

                // Triangle 1
                triangles[tris + 0] = current;
                triangles[tris + 1] = next;
                triangles[tris + 2] = current + 1;

                // Triangle 2
                triangles[tris + 3] = current + 1;
                triangles[tris + 4] = next;
                triangles[tris + 5] = next + 1;

                tris += 6;
            }
        }

        // Connecter les parallèles les plus bas avec le pôle sud
        int baseIndex = (nbParalleles - 2) * (nbMeridiens + 1) + 1;
        for (int i = 0; i < nbMeridiens; i++)
        {
            triangles[tris + 0] = baseIndex + i;
            triangles[tris + 1] = vertices.Length - 1;
            triangles[tris + 2] = baseIndex + (i + 1) % nbMeridiens;

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

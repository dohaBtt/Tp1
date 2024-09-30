# TP1 : Polyèdres et Quadriques

## Aperçu

Ce dépôt contient le code source et l'implémentation du TP1, qui porte sur la modélisation de polyèdres et de quadriques en utilisant Unity. Le projet consiste à créer des primitives géométriques telles que des plans, des cylindres, des sphères, et optionnellement des cônes ou des objets tronqués.

Vous trouverez ci-dessous la structure du code et des explications pour chaque partie du TP.

## Table des matières
- [Aperçu](#aperçu)
- [Prérequis](#prérequis)
- [Structure du projet](#structure-du-projet)
- [Instructions](#instructions)
  - [Exercice 0 : Plan](#exercice-0-plan)
  - [Exercice I : Cylindre](#exercice-i-cylindre)
  - [Exercice II : Sphère](#exercice-ii-sphère)
  - [Optionnel : Exercice III : Cône](#optionnel-exercice-iii-cone)
  - [Optionnel : Exercice IV : Objets Tronqués](#optionnel-exercice-iv-objets-tronqués)
- [Exécution](#exécution)
- [Évaluation](#évaluation)
- [Licence](#licence)

## Prérequis

Pour exécuter ce projet, vous aurez besoin de :
- Unity 2020 ou une version plus récente
- Visual Studio Code ou tout autre éditeur de code supportant les scripts C#
- Git pour le contrôle de version

## Structure du projet

Voici un aperçu des fichiers et des répertoires dans le projet :

```plaintext
.
├── .vscode/            # Contient les fichiers de configuration de VSCode
├── Assets/             # Contient tous les assets pour le projet Unity
│   ├── Scenes/         # Contient la ou les scènes pour ce projet
│   └── Script/         # Contient les scripts C# pour chaque exercice
│       ├── Cone.cs         # Implémentation du cône
│       ├── Cylindre.cs     # Implémentation du cylindre
│       ├── Sphere.cs       # Implémentation de la sphère
│       └── TriangleGrid.cs # Grille de triangles pour le plan
├── Packages/           # Dépendances des packages Unity
├── ProjectSettings/    # Paramètres du projet Unity
├── .gitignore          # Fichier Git ignore
├── README.md           # Documentation du projet (ce fichier)
└── .vsconfig           # Configuration de VSCode pour Unity
# Exercice 0 : Plan
**Objectif :** Créer un plan en utilisant deux triangles.  
**Détails :** Vous pouvez étendre cet exercice en augmentant le nombre de triangles, organisés en lignes et colonnes.  
**Code :** `TriangleGrid.cs` gère la construction du plan. Ajustez le nombre de lignes et colonnes comme paramètres pour affiner la densité de la grille.

---

# Exercice I : Cylindre
**Objectif :** Modéliser un cylindre et le décomposer en facettes triangulaires.  
**Détails :** Le script `Cylindre.cs` contient la méthode pour générer un cylindre avec des paramètres pour le rayon, la hauteur et le nombre de méridiens. Le cylindre inclut également des "couvercles" en haut et en bas.  


---

# Exercice II : Sphère
**Objectif :** Modéliser une sphère.  
**Détails :** Dans `Sphere.cs`, nous définissons la méthode pour générer une sphère en fonction du rayon, des parallèles et des méridiens. La gestion des pôles (Nord et Sud) est cruciale pour un rendu lisse.

---

# Optionnel : Exercice III : Cône
**Objectif :** Modéliser un cône (tronqué ou non).  
**Détails :** Le script `Cone.cs` gère la création du cône, avec des paramètres pour ajuster le rayon, la hauteur et le niveau de troncature (optionnel). Vous pouvez passer ces valeurs pour générer différentes variantes du cône.

---

# Optionnel : Exercice IV : Objets Tronqués
**Objectif :** Implémenter des versions tronquées des objets précédents (ex. sphère tronquée, cône).  
**Détails :** Les fichiers de script prennent également en charge la troncature d'objets comme les sphères (`SphereTronquee.cs`), les cônes et autres quadriques.

---

# Exécution
Clonez ce dépôt :

```bash
git clone https://github.com/dohaBtt/Tp1.git


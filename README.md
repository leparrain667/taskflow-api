# TaskFlow API

> API REST de gestion de projets et de tâches, développée en **ASP.NET Core 8** avec **Entity Framework Core**, **JWT** et **SQL Server**.

Projet réalisé dans le cadre de ma formation en développement .NET — **Souleymane Traoré**.

---

## Stack technique

| Technologie | Rôle |
|---|---|
| ASP.NET Core 8 | Framework web / REST API |
| Entity Framework Core | ORM / accès base de données |
| SQL Server (LocalDB) | Base de données relationnelle |
| JWT (JSON Web Tokens) | Authentification stateless |
| BCrypt | Hachage des mots de passe |
| Swagger / OpenAPI | Documentation interactive |

---

## Fonctionnalités

- **Authentification JWT** — inscription, connexion, token sécurisé
- **Gestion des projets** — CRUD complet, accès restreint au propriétaire
- **Gestion des tâches** — CRUD, statuts (À faire / En cours / Terminé), date d'échéance, commentaires
- **Contrôle des accès** — chaque utilisateur ne voit et ne modifie que ses propres données
- **Gestion globale des erreurs** — middleware centralisé, codes HTTP sémantiques
- **Architecture en couches** — Controllers → Services (interfaces) → Repository (EF Core)

---

## Architecture du projet

```
TaskFlow.API/
├── Controllers/          → Endpoints REST (Users, Projects, Tasks)
├── Models/               → Entités EF Core
├── DTOs/                 → Objets de transfert (découplage API / BDD)
├── Services/             → Logique métier (interfaces + implémentations)
├── Data/                 → ApplicationDbContext
├── Middlewares/          → Gestion globale des exceptions
├── Migrations/           → Migrations EF Core
└── Program.cs            → Configuration (DI, JWT, Swagger, EF)
```

**Modèle de données :**
```
User (1) ────< Project (N) ────< TaskItem (N)
```

---

## Installation & lancement

### Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server ou SQL Server Express / LocalDB (inclus avec Visual Studio)

### Étapes

```bash
# 1. Cloner le dépôt
git clone https://github.com/leparrain667/taskflow-api.git
cd taskflow-api/taskflow-api.git
cd taskflow-api/TaskFlow.API

# 2. Restaurer les dépendances
dotnet restore

# 3. Configurer les secrets (clé JWT)
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "VotreCleSecreteMinimum32Caracteres!!"

# 4. Créer la base de données
dotnet tool install --global dotnet-ef   # si pas déjà installé
dotnet ef database update

# 5. Lancer l'API
dotnet run
```

L'interface Swagger est accessible à : `https://localhost:5001/swagger`

> **Note sécurité** : La clé JWT et les secrets ne sont jamais commités dans ce dépôt.  
> Utiliser `dotnet user-secrets` en développement ou des variables d'environnement en production.

---

## Endpoints

### Authentification (public)

| Méthode | Endpoint | Description |
|---|---|---|
| POST | `/api/users/register` | Créer un compte |
| POST | `/api/users/login` | Se connecter → reçoit un JWT |

### Projets (JWT requis)

| Méthode | Endpoint | Description |
|---|---|---|
| GET | `/api/projects` | Lister ses projets |
| POST | `/api/projects` | Créer un projet |
| GET | `/api/projects/{id}` | Détail d'un projet |
| PUT | `/api/projects/{id}` | Modifier (propriétaire uniquement) |
| DELETE | `/api/projects/{id}` | Supprimer (propriétaire uniquement) |

### Tâches (JWT requis)

| Méthode | Endpoint | Description |
|---|---|---|
| GET | `/api/tasks` | Lister ses tâches |
| POST | `/api/tasks` | Créer une tâche |
| GET | `/api/tasks/{id}` | Détail d'une tâche |
| PUT | `/api/tasks/{id}` | Modifier une tâche |
| DELETE | `/api/tasks/{id}` | Supprimer une tâche |

### Codes HTTP sémantiques

| Code | Signification |
|---|---|
| 200 | Succès (GET / PUT) |
| 201 | Ressource créée (POST) |
| 204 | Suppression réussie (DELETE) |
| 400 | Données invalides |
| 401 | Non authentifié |
| 403 | Accès interdit (pas propriétaire) |
| 404 | Ressource introuvable |
| 500 | Erreur serveur |

---

## Utilisation avec Swagger

1. Appeler `POST /api/users/register` puis `POST /api/users/login`
2. Copier le `token` reçu dans la réponse
3. Cliquer sur **Authorize** dans Swagger et coller `Bearer <token>`
4. Toutes les routes protégées sont désormais accessibles

---

## Auteur

**Souleymane Traoré** — étudiant en développement logiciel, en recherche d'alternance ou de stage.

[![LinkedIn](https://img.shields.io/badge/LinkedIn-Profil-blue?logo=linkedin)](https://linkedin.com/in/votre-profil)

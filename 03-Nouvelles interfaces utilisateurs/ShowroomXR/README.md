# Showroom de design immersif

L'objectif était de concevoir une expérience 3D pensée comme une application de réalité virtuelle, tout en restant utilisable sur un ordinateur, sans casque. J'ai choisi le cas d'usage d'un showroom de design : une galerie dans laquelle on circule librement, où l'on peut manipuler certains objets, consulter les informations des produits exposés et personnaliser l'un d'entre eux.

Le projet a été développé avec Unity 6.6 (Universal Render Pipeline) et le XR Interaction Toolkit 3.6.1. Le casque et les manettes sont simulés au clavier et à la souris grâce au XR Device Simulator.

## Lancer le projet

Les deux versions compilées se trouvent dans le dossier `Builds` :

- **Mac** : décompresser `GalerieDeAmbre-Mac.zip` puis ouvrir l'application. Si macOS la bloque au premier lancement, faire un clic droit puis « Ouvrir ».
- **Windows** : décompresser `GalerieDeAmbre-Windows.zip` puis lancer `GalerieDeAmbre.exe`. Le dossier doit rester complet, l'exécutable ne fonctionne pas seul.

Pour ouvrir le projet source, il suffit de l'ajouter dans Unity Hub (version 6000.6.3f1) puis d'ouvrir la scène `Assets/_Project/Scenes/Showroom`. Au premier chargement, Unity reconstruit automatiquement son cache, ce qui peut prendre quelques minutes.

## Contrôles

Le simulateur reproduit un casque et deux manettes. La souris ne contrôle qu'un seul élément à la fois : la tête ou l'une des deux mains. Un panneau d'aide reste affiché en bas à gauche de l'écran.

| Action | Touche (clavier AZERTY) |
|---|---|
| Regarder autour de soi | U, puis la souris |
| Contrôler la main droite / gauche | Y / T (ou maintenir Espace / Maj) |
| Se téléporter | Y, maintenir Z, viser le sol ou un disque, relâcher |
| Marcher | I, J, K, L |
| Attraper ou lâcher un objet | viser l'objet, puis G |
| Appuyer sur un bouton | viser le bouton, puis clic gauche |

Chaque main conserve sa position lorsqu'on ne la contrôle plus. Si une main reste pointée sur un objet, celui-ci reste en surbrillance : il suffit de la reprendre et de la diriger vers le sol.

## L'expérience

La galerie est organisée en trois zones, reliées par la téléportation. On peut se téléporter n'importe où sur le sol, ou sur les disques bordeaux placés devant chaque zone.

**L'accueil.** Un panneau de bienvenue présente la galerie et les contrôles ; le bouton « Commencer la visite » le fait disparaître. Deux vases en céramique posés sur la table peuvent être saisis, observés sous tous les angles, reposés ou lancés.

**La zone produit.** Un fauteuil Mid-Century est exposé sur un socle. Le configurateur placé à sa droite permet de changer sa finition (Original, Ébène, Bordeaux, Miel) et de le faire pivoter sur lui-même. Lorsqu'on vise le fauteuil, sa fiche produit apparaît à côté de lui.

**La zone découverte.** Une lampe d'architecte est accompagnée d'un bouton posé sur le socle. En appuyant dessus, le bouton s'enfonce et la lampe s'allume progressivement. L'étiquette au-dessus du bouton indique l'action disponible (ALLUMER ou ÉTEINDRE), ce qui renseigne en permanence sur l'état de la lampe. Elle dispose elle aussi d'une fiche produit.

L'expérience réunit ainsi cinq types d'interactions (téléportation, manipulation d'objets, sélection à distance, activation d'une action et utilisation d'interfaces) et quatre interfaces spatiales : le panneau d'accueil, le configurateur et les deux fiches produits.

## Retours utilisateur

Une seule couleur d'accent, un bordeaux, est réservée aux éléments interactifs : disques de téléportation, boutons et surbrillance des objets. L'utilisateur peut ainsi identifier d'un coup d'œil ce qui peut être utilisé.

Chaque interaction produit un retour visible. Un objet visé s'illumine et grossit légèrement, et sa lueur s'intensifie lorsqu'on le tient. Les boutons des panneaux s'éclaircissent au survol et s'assombrissent au clic. Le bouton de la lampe s'enfonce physiquement, et la lampe s'allume en fondu plutôt que brutalement. Le changement de finition du fauteuil se fait par une courte transition de couleur, et les fiches produits apparaissent et disparaissent en fondu. Enfin, le rayon des manettes change de couleur dès qu'il touche un élément utilisable.

## Choix de conception

**Navigation.** La téléportation est le mode de déplacement principal, car le déplacement continu au joystick provoque facilement des nausées en VR. La marche reste disponible sur la main gauche pour les petits ajustements. Les points d'arrivée placent l'utilisateur à bonne distance de chaque objet et déjà orienté vers lui.

**Interactions.** Chaque touche correspond à une intention unique : G sert toujours à saisir, le clic gauche sert toujours à activer. La saisie fonctionne par bascule (un appui pour attraper, un appui pour lâcher), car maintenir une touche tout en visant à la souris est inconfortable. Le fauteuil et la lampe réagissent au regard mais ne peuvent pas être saisis, comme dans un véritable showroom.

**Placement des informations.** Les panneaux à lire sont à hauteur des yeux (1,6 m) et ceux que l'on manipule à hauteur de main (1,4 m). Lors des premiers tests, le panneau d'accueil placé à plus de 3 m était difficile à lire ; il a été rapproché à environ 2,6 m et agrandi. Les panneaux sont toujours placés sur les côtés des objets pour ne jamais les masquer, et les fiches produits pivotent pour rester face à l'utilisateur. Les fonds légèrement transparents évitent l'effet de mur au milieu de la pièce, et la hiérarchie visuelle reste simple : titre, sous-titre, puis détails alignés, avec le prix mis en valeur.

**Éclairage.** La lumière générale est volontairement tamisée, et un spot au plafond éclaire chaque zone. En plus de créer une ambiance de galerie, cet éclairage oriente naturellement le regard vers les objets exposés et rend visible l'allumage de la lampe.

## Interface 2D et interface immersive

### Une interface située dans l'espace

Sur un écran, l'interface occupe un cadre fixe et l'ensemble des informations est visible d'un seul regard. En immersion, chaque élément possède une position, une distance, une orientation et une taille réelle. Un panneau peut se trouver derrière l'utilisateur ou en dehors de son champ de vision. La conception doit donc prendre en compte l'ergonomie physique (hauteur, distance de lecture, angle de vue) et guider activement l'attention, par la lumière ou en plaçant l'information au plus près de l'objet qu'elle concerne.

### Un pointage moins précis

La souris offre une précision au pixel près, alors que le rayon tenu à la main est instable. Les cibles doivent être plus grandes et le retour au survol devient indispensable, puisque l'utilisateur ne ressent aucun contact physique. C'est pour cette raison que les fiches produits restent affichées une demi-seconde après la fin du survol : sans ce délai, elles clignoteraient au moindre tremblement de la main.

### La navigation devient un déplacement

Dans une interface 2D, naviguer consiste à changer de page ou à faire défiler un contenu. En immersion, l'utilisateur déplace son propre point de vue, ce qui soulève des questions absentes d'un écran : le confort (d'où le choix de la téléportation), l'orientation à l'arrivée, et la lisibilité du contenu depuis la position où l'on se trouve.

### Une interface intégrée au décor

En 2D, un menu se superpose simplement au contenu. En VR, un panneau opaque placé devant les yeux rompt le sentiment de présence. Les interfaces doivent s'intégrer à l'environnement : c'est pourquoi les panneaux de la galerie partagent le style du lieu, restent légèrement transparents et n'apparaissent que lorsqu'ils sont utiles.

## Organisation du projet

```
ShowroomXR
├── Assets
│   ├── _Project
│   │   ├── Art
│   │   │   ├── Materials
│   │   │   ├── Models
│   │   │   │   ├── LoungeChair
│   │   │   │   ├── Lamp
│   │   │   │   ├── Vase01
│   │   │   │   └── Vase02
│   │   │   └── Textures
│   │   ├── Scenes
│   │   └── Scripts
│   ├── Samples
│   ├── Settings
│   ├── TextMesh Pro
│   └── XRI
├── Builds
├── Captures
├── Packages
└── ProjectSettings
```

**`Assets/_Project`** regroupe tout ce qui a été créé pour ce projet. Le tiret bas permet de le placer en tête de liste et de le distinguer des dossiers générés par Unity ou par les packages.

- `Art/Materials` contient les matériaux de la galerie : `M_Sol` (parquet), `M_Mur` (murs et plafond), `M_Socle` (socles et table) et `M_Accent` (le bordeaux des éléments interactifs).
- `Art/Models` contient les modèles 3D importés, chacun dans son propre dossier avec ses textures, pour éviter les conflits entre fichiers portant le même nom.
- `Art/Textures` contient les textures du sol et des murs.
- `Scenes` contient la scène principale, `Showroom`.
- `Scripts` contient les scripts C# du projet :
  - `InteractionFeedback` gère la surbrillance des objets visés ou saisis ;
  - `ProductConfigurator` gère les finitions et la rotation du fauteuil ;
  - `ProductInfoDisplay` gère l'apparition, la disparition et l'orientation des fiches produits ;
  - `PressableButton` gère le bouton 3D et son animation ;
  - `LampController` gère l'allumage de la lampe et l'étiquette d'état.

**Les autres dossiers de `Assets`** proviennent d'Unity et des packages, et ne doivent pas être modifiés. `Samples` contient les ressources fournies par le XR Interaction Toolkit (le rig XR et le simulateur), `Settings` les réglages du rendu URP, `TextMesh Pro` les ressources de texte et `XRI` la configuration du toolkit.

**À la racine du projet**, `Builds` contient les versions compilées pour Mac et Windows, `Captures` les captures d'écran annotées, `Packages` la liste des packages utilisés et `ProjectSettings` les réglages du projet. Les dossiers `Library`, `Logs`, `Temp` et `UserSettings` ne sont pas versionnés : Unity les recrée automatiquement à l'ouverture.

Dans la scène, les objets sont rangés en quatre groupes : `XR` (le rig et le simulateur), `Environnement` (sol, murs, plafond), `Eclairage` (lumières et post-traitement) et `Zones`, qui contient une entrée par zone avec son mobilier, ses objets, ses interfaces et son point de téléportation.

## Plan de la galerie

```
                              MUR NORD
+----------------------------------------------------------------+
|                                                                |
|  [Fiche] (FAUTEUIL) [Config.]      [Bouton 3D] (LAMPE) [Fiche] |
|           * spot                               * spot          |
|           o arrivée                            o arrivée       |
|         ZONE PRODUIT                      ZONE DÉCOUVERTE      |
|                                                                |
|                   [ PANNEAU DE BIENVENUE ]                     |
|                   (TABLE + 2 VASES)  * spot                    |
|                      o arrivée / départ                        |
|                        ZONE ACCUEIL                            |
+----------------------------------------------------------------+
                               MUR SUD

o = point de téléportation    * = spot au plafond
[ ] = interface               ( ) = objet exposé / mobilier
```

Les captures annotées se trouvent dans le dossier `Captures`.

## Limites et pistes d'amélioration

Le simulateur reste une approximation de l'expérience réelle : une seule main est contrôlable à la fois, les manettes ne vibrent pas et le texte paraît plus petit à l'écran que dans un casque. Avec plus de temps, j'aurais ajouté dans le configurateur une indication de la finition sélectionnée, ainsi qu'un libellé du bouton de rotation qui change lorsque le fauteuil tourne.

## Crédits

Modèles et textures issus de [Poly Haven](https://polyhaven.com), sous licence CC0 : Mid Century Lounge Chair (Kuutti Siitonen), Desk Lamp Arm 01, Ceramic Vase 01 et 02, texture Lacquered Cherry Wood.

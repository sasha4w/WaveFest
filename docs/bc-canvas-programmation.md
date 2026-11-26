# Bounded Context Canvas : Programmation

Version formulaire (modèle ddd-crew rempli) : [bc-canvas-programmation.html](bc-canvas-programmation.html)

## Nom et objectif

**Programmation**

Permettre à l'organisation de préparer chaque festival : ses jours, ses têtes d'affiche et ses vagues de vente. Elle annonce aux autres contextes ce qui est programmé et ce qui est annulé.

## Classification

| Axe | Choix | Pourquoi |
| --- | --- | --- |
| Type de domaine | **Supporting** | Nécessaire à la vente mais sans règle différenciante : surtout de la saisie par l'organisation. |
| Modèle économique | **Engagement** | L'affiche est ce qui donne envie d'acheter un pass. |
| Évolution | **Custom built** | Modèle simple, construit pour alimenter la Billetterie. |

## Rôle

Specification context : décrit le festival que les autres contextes exécutent.

## Langage ubiquitaire

| Terme | Définition dans ce contexte |
| --- | --- |
| Festival | Évènement de un ou plusieurs jours dont les pass sont vendus par l'application. |
| Organisation | Groupe responsable de la gestion et de l'organisation du festival. |
| Programmation | Préparation et mise en place d'un évènement. |
| Jour | Journée du festival, avec sa tête d'affiche. |
| Tête d'affiche | Artiste principal annoncé pour un jour donné. |
| Concert | Passage d'un artiste un jour donné. |
| Annulation | Décision de l'organisation de retirer un concert du programme. |
| Vague | Période de vente définie par un prix, un quota et une date limite. |
| Quota | Nombre de pass mis en vente dans une vague. |
| Date limite | Date à laquelle une vague se clôt si son quota n'est pas épuisé. |

## Règles métier (invariants)

- L'application gère plusieurs festivals en même temps.
- Un festival a une tête d'affiche par jour.
- Chaque festival a trois vagues : Early, Regular, Last minute, dans cet ordre.
- Une vague est définie par un prix, un quota et une date limite.
- Les dates limites des vagues se suivent sans se chevaucher.
- Seule l'organisation peut programmer ou annuler un concert.
- Toute annulation de tête d'affiche est publiée pour déclencher les remboursements.

## Entrées

| Type | Message | Émetteur | Relation |
| --- | --- | --- | --- |
| Commande | Programmer un festival | Organisation | Acteur |
| Commande | Définir les vagues | Organisation | Acteur |
| Commande | Annuler un concert | Organisation | Acteur |
| Requête | Programme du festival | Festivalier | Acteur |

## Sorties

| Type | Message | Destinataire | Relation |
| --- | --- | --- | --- |
| Événement | Vagues définies | Billetterie | Customer/Supplier (D) |
| Événement | Concert annulé | Remboursement billet | Customer/Supplier (D) |
| Événement | Festival programmé (dates) | Gestion de billet | Customer/Supplier (D) |

Dans la colonne Relation, (U) veut dire que l'autre contexte est en amont, (D) qu'il est en aval.

## Hypothèses

- Les vagues sont définies ici et appliquées par la Billetterie.
- Les liens vers Remboursement billet et Gestion de billet, sans pattern sur la Context Map, sont du Customer/Supplier avec Programmation en amont.
- Classé Supporting alors que le tableau du mur range le « CRUD d'évènement et de vagues » en Generic.

## Métriques de vérification

- Nombre de corrections manuelles de vagues après l'ouverture de la vente.
- Délai entre la décision d'annuler et l'évènement « Concert annulé » publié.
- Nombre de fois où la Billetterie a dû demander une évolution du modèle de vague.

## Questions ouvertes

- Peut-on modifier le prix ou le quota d'une vague déjà ouverte ?
- Que se passe-t-il si une tête d'affiche est remplacée plutôt qu'annulée ?
- L'annulation d'un artiste qui n'est pas tête d'affiche donne-t-elle droit à quelque chose ?
- Qui définit les vagues : la Programmation ou la Billetterie ?

# Bounded Context Canvas : Remboursement billet

Version formulaire (modèle ddd-crew rempli) : [bc-canvas-remboursement-billet.html](bc-canvas-remboursement-billet.html)

## Nom et objectif

**Remboursement billet**

Rembourser les festivaliers quand une tête d'affiche est annulée, en proportion des jours touchés. Elle calcule le montant dû pour chaque pass et le fait verser par le PSP.

## Classification

| Axe | Choix | Pourquoi |
| --- | --- | --- |
| Type de domaine | **Supporting** | Obligatoire en cas d'annulation, mais ce n'est pas ce qui fait vendre. |
| Modèle économique | **Compliance** | Rembourser est une obligation envers le festivalier. |
| Évolution | **Custom built** | La règle de calcul par jour annulé est propre à WaveFest. |

## Rôle

Execution context : réagit à une annulation et mène les remboursements à leur terme.

## Langage ubiquitaire

| Terme | Définition dans ce contexte |
| --- | --- |
| Remboursement | Restitution partielle du montant payé pour un pass. |
| Concert annulé | Tête d'affiche retirée du programme d'un jour. |
| Jour annulé | Jour du festival dont la tête d'affiche est annulée. |
| Pass concerné | Pass qui donne accès à un jour annulé. |
| Prix payé | Montant réglé pour le pass à l'achat. |
| Montant remboursé | Prix payé / nombre de jours du pass / 2, par jour annulé. |
| Bénéficiaire | Festivalier qui reçoit le remboursement. |
| Échec PSP | Remboursement que le PSP n'a pas pu verser. |
| Nouvelle tentative | Relance d'un remboursement en échec. |
| Tête d'affiche | Artiste principal annoncé pour un jour donné. |

## Règles métier (invariants)

- L'annulation d'une tête d'affiche ouvre droit à un remboursement pour tous les pass du jour concerné.
- Pass 1 jour : remboursé à 50 %.
- Pass 3 jours : 1/6 du prix par jour annulé (1 jour 1/6, 2 jours 1/3, 3 jours 1/2).
- Formule : prix payé / nombre de jours du pass / 2, par jour annulé.
- Un pass n'est remboursé qu'une fois par jour annulé.
- Le remboursement est automatique, le festivalier n'a rien à demander.
- Si le PSP échoue, le remboursement est retenté jusqu'à réussite.
- Le festivalier est prévenu par e-mail de l'annulation et du remboursement.

## Entrées

| Type | Message | Émetteur | Relation |
| --- | --- | --- | --- |
| Commande | Relancer le remboursement | Politique échec PSP | Interne |
| Événement | Concert annulé | Programmation | Customer/Supplier (U) |
| Événement | Remboursement versé / échoué | PSP | ACL (U, externe) |
| Requête | Suivi de mon remboursement | Festivalier | Acteur |

## Sorties

| Type | Message | Destinataire | Relation |
| --- | --- | --- | --- |
| Commande | Rembourser le festivalier | PSP | ACL (U, externe) |
| Commande | Envoyer l'e-mail d'annulation | Notifications | Conformist (U) |
| Événement | Pass remboursé | Notifications | Conformist (U) |
| Requête | Prix payé de la commande | Billetterie | Shared Kernel |

Dans la colonne Relation, (U) veut dire que l'autre contexte est en amont, (D) qu'il est en aval.

## Hypothèses

- Le Shared Kernel avec la Billetterie porte sur la commande et le prix payé.
- Le lien avec Programmation, sans pattern sur la Context Map, est du Customer/Supplier avec Programmation en amont.
- Le pass VIP est remboursé selon la même formule, avec son nombre de jours.
- Le remboursement est versé sur le moyen de paiement d'origine.

## Métriques de vérification

- Délai entre « Concert annulé » et le dernier remboursement versé.
- Part des remboursements en échec au premier essai.
- Écart entre le total remboursé et le total calculé : doit rester à 0.

## Questions ouvertes

- Billet revendu avant l'annulation : on rembourse le vendeur ou le titulaire actuel, et sur quel prix ?
- Combien de tentatives après un échec du PSP, et que fait-on ensuite ?
- Le pass VIP couvre combien de jours ?
- Un billet déjà utilisé à l'entrée est-il remboursé ?
- Le Shared Kernel avec la Billetterie est-il tenable, ou faut-il un Customer/Supplier ?

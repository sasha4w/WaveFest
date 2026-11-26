# Bounded Context Canvas : Remboursement cashless

Version formulaire (modèle ddd-crew rempli) : [bc-canvas-remboursement-cashless.html](bc-canvas-remboursement-cashless.html)

## Nom et objectif

**Remboursement cashless**

Rendre au festivalier les crédits non dépensés sur son bracelet après le festival. Elle prélève les frais prévus et fait verser le reste par le PSP.

## Classification

| Axe | Choix | Pourquoi |
| --- | --- | --- |
| Type de domaine | **Supporting** | Suite obligatoire du cashless, sans valeur différenciante. |
| Modèle économique | **Compliance** | Le solde restant appartient au festivalier. |
| Évolution | **Custom built** | Le délai et les frais sont propres à WaveFest. |

## Rôle

Execution context : solde les comptes cashless après le festival.

## Langage ubiquitaire

| Terme | Définition dans ce contexte |
| --- | --- |
| Remboursement | Restitution du solde restant d'un compte cashless. |
| Solde restant | Crédits non dépensés à la fin du festival. |
| Frais | 1,50 € prélevés sur chaque remboursement. |
| Montant remboursé | Solde restant moins les frais. |
| Délai | 15 jours après le festival. |
| Compte cashless | Compte rattaché à un bracelet, qui porte le solde. |
| Bracelet | Support RFID associé au compte cashless. |
| Bénéficiaire | Festivalier qui reçoit le remboursement. |
| Échec PSP | Remboursement que le PSP n'a pas pu verser. |
| Compte soldé | Compte cashless dont le solde a été remboursé. |

## Règles métier (invariants)

- Le solde restant est remboursé 15 jours après le festival.
- 1,50 € de frais sont prélevés sur chaque remboursement.
- Le montant remboursé est le solde restant moins les frais.
- Un compte cashless n'est remboursé qu'une seule fois.
- Un compte remboursé ne peut plus être rechargé ni débité.
- Si le PSP échoue, le remboursement est retenté.

## Entrées

| Type | Message | Émetteur | Relation |
| --- | --- | --- | --- |
| Commande | Rembourser les soldes restants | Politique J+15 | Interne |
| Commande | Relancer le remboursement | Politique échec PSP | Interne |
| Événement | Achat encaissé | Paiement cashless | Shared Kernel |
| Événement | Remboursement versé / échoué | PSP | ACL (U, externe) |
| Requête | Suivi de mon remboursement | Festivalier | Acteur |

## Sorties

| Type | Message | Destinataire | Relation |
| --- | --- | --- | --- |
| Commande | Rembourser le festivalier | PSP | ACL (U, externe) |
| Événement | Solde remboursé | Notifications | Conformist (U) |
| Requête | Solde restant | Paiement cashless | Shared Kernel |

Dans la colonne Relation, (U) veut dire que l'autre contexte est en amont, (D) qu'il est en aval.

## Hypothèses

- Le remboursement est automatique, sans demande du festivalier.
- « 15 j après » sur le mur veut dire versé 15 jours après la fin du festival.
- Un solde inférieur ou égal aux frais ne donne lieu à aucun versement.
- Le remboursement est versé sur le moyen de paiement du rechargement.

## Métriques de vérification

- Part des comptes soldés à J+15.
- Part des remboursements en échec au premier essai.
- Écart entre soldes restants, frais et montants versés : doit rester à 0.

## Questions ouvertes

- Le remboursement est-il automatique ou sur demande du festivalier ?
- « 15 j » : versé après 15 jours, ou à demander dans les 15 jours ?
- Que fait-on d'un solde inférieur à 1,50 € ?
- Rechargement en espèces sur place : sur quel compte rembourser ?
- Faut-il vraiment deux contextes cashless reliés par un Shared Kernel ?

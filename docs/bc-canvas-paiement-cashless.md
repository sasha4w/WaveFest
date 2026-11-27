# Bounded Context Canvas : Paiement cashless

Version formulaire (modèle ddd-crew rempli) : [bc-canvas-paiement-cashless.html](bc-canvas-paiement-cashless.html)

## Nom et objectif

**Paiement cashless**

Permettre au festivalier de payer sur le site sans espèces, avec son bracelet. Elle tient le solde de chaque bracelet : rechargements d'un côté, achats de l'autre.

## Classification

| Axe | Choix | Pourquoi |
| --- | --- | --- |
| Type de domaine | **Core** | Classé Core sur le mur : toute la dépense sur site passe par là. |
| Modèle économique | **Revenue** | Chaque achat sur le site passe par le bracelet. |
| Évolution | **Product** | Confié à un prestataire externe. |

## Rôle

Execution context : enregistre rechargements et achats et tient le solde.

## Langage ubiquitaire

| Terme | Définition dans ce contexte |
| --- | --- |
| Cashless | Forme de paiement sans espèces au sein du festival. |
| Bracelet | Support RFID qui sert de moyen de paiement. |
| Compte cashless | Compte rattaché à un bracelet, qui porte le solde. |
| Solde | Crédits disponibles sur le compte cashless. |
| Rechargement | Ajout de crédits sur le compte cashless. |
| Achat | Paiement d'une consommation avec le bracelet. |
| Point de vente | Stand ou bar qui encaisse par bracelet. |
| Vendeur | Personne qui encaisse un achat sur un point de vente. |
| Solde insuffisant | Motif de refus d'un achat. |
| Solde restant | Crédits non dépensés à la fin du festival. |

## Règles métier (invariants)

- Un bracelet a un seul compte cashless.
- Le solde n'est jamais négatif.
- Un achat est refusé si le solde est insuffisant.
- Un rechargement n'est crédité qu'après paiement confirmé.
- Seul un bracelet remis à l'entrée peut être rechargé ou débité.
- Chaque achat et chaque rechargement est tracé.
- Les achats s'arrêtent à la fin du festival, le solde restant part au remboursement.

## Entrées

| Type | Message | Émetteur | Relation |
| --- | --- | --- | --- |
| Commande | Recharger le bracelet | Festivalier | Acteur |
| Commande | Encaisser un achat | Vendeur | Acteur |
| Événement | Bracelet remis | Contrôle d'accès | Partnership |
| Événement | Paiement confirmé / refusé | PSP | ACL (U, externe) |
| Requête | Consulter le solde | Festivalier | Acteur |
| Requête | Solde restant | Remboursement cashless | Shared Kernel |

## Sorties

| Type | Message | Destinataire | Relation |
| --- | --- | --- | --- |
| Commande | Demander le paiement | PSP | ACL (U, externe) |
| Événement | Bracelet rechargé | Notifications | Conformist (U) |
| Événement | Achat encaissé | Remboursement cashless | Shared Kernel |

Dans la colonne Relation, (U) veut dire que l'autre contexte est en amont, (D) qu'il est en aval.

## Hypothèses

- Le compte cashless est ouvert quand le bracelet est remis à l'entrée.
- Le Shared Kernel avec Remboursement cashless porte sur le compte cashless et son solde.
- Le prestataire externe fournit le matériel, les règles de solde restent chez WaveFest.

## Métriques de vérification

- Part des achats refusés pour solde insuffisant.
- Temps d'encaissement d'un achat.
- Écart entre rechargements, achats et soldes : doit rester à 0.

## Questions ouvertes

- Classé Core mais confié à un prestataire externe : que garde-t-on vraiment chez nous ?
- Peut-on recharger en ligne avant le festival, ou seulement sur place ?
- Les points de vente doivent-ils fonctionner sans réseau ?
- Bracelet perdu : peut-on bloquer le compte et récupérer le solde ?
- Y a-t-il un montant minimum ou maximum de rechargement ?

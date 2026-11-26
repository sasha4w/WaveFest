# Bounded Context Canvas : Gestion de billet

Version formulaire (modèle ddd-crew rempli) : [bc-canvas-gestion-de-billet.html](bc-canvas-gestion-de-billet.html)

## Nom et objectif

**Gestion de billet**

Faire vivre le billet après l'achat : l'envoyer au festivalier et tenir à jour le nom de son titulaire. Elle garantit qu'un billet reste nominatif jusqu'à l'entrée.

## Classification

| Axe | Choix | Pourquoi |
| --- | --- | --- |
| Type de domaine | **Supporting** | Indispensable au festivalier mais peu de règles propres à WaveFest. |
| Modèle économique | **Engagement** | C'est le service après-vente du billet ; le changement de nom payant reste marginal. |
| Évolution | **Custom built** | Les règles de J-7 et d'envoi sont celles du festival. |

## Rôle

Execution context : applique les changements demandés sur un billet existant.

## Langage ubiquitaire

| Terme | Définition dans ce contexte |
| --- | --- |
| Billet | Titre d'accès nominatif présenté à l'entrée. |
| Titulaire | Personne dont le nom figure sur le billet. |
| Billet nominatif | Billet valable uniquement pour son titulaire. |
| Changement de nom | Remplacement du titulaire d'un billet. |
| J-7 | Sept jours avant le début du festival, fin du changement de nom gratuit. |
| Envoi | Remise du billet au festivalier par e-mail, SMS ou courrier. |
| Canal d'envoi | Moyen choisi pour recevoir le billet. |
| Compte | Compte utilisateur auquel le billet est rattaché. |
| Frais de changement | Montant demandé pour un changement de nom après J-7. |
| Festivalier | Personne qui détient le billet sur son compte. |

## Règles métier (invariants)

- Tout billet porte le nom d'un seul titulaire.
- Tout billet est rattaché à un compte utilisateur.
- Le changement de nom est gratuit jusqu'à J-7.
- Après J-7, le changement de nom devient payant.
- Un billet déjà utilisé à l'entrée ne peut plus changer de nom.
- Le billet est envoyé dès qu'il est émis, par le canal choisi par le festivalier.
- Après un changement de nom, le billet est renvoyé à son titulaire.

## Entrées

| Type | Message | Émetteur | Relation |
| --- | --- | --- | --- |
| Commande | Changer le nom du billet | Festivalier | Acteur |
| Événement | Billets émis | Billetterie | Customer/Supplier (U) |
| Événement | Festival programmé (dates) | Programmation | Customer/Supplier (U) |
| Événement | Paiement confirmé / refusé | PSP | ACL (U, externe) |
| Requête | Consulter mon billet | Festivalier | Acteur |

## Sorties

| Type | Message | Destinataire | Relation |
| --- | --- | --- | --- |
| Commande | Envoyer le billet | Notifications | Conformist (U) |
| Commande | Demander le paiement | PSP | ACL (U, externe) |
| Événement | Nom du billet changé | Notifications | Conformist (U) |

Dans la colonne Relation, (U) veut dire que l'autre contexte est en amont, (D) qu'il est en aval.

## Hypothèses

- Sur la Context Map, la Billetterie est en amont de Gestion de billet.
- Le lien avec Programmation sert à connaître la date du festival pour calculer J-7.
- L'envoi (e-mail, SMS, courrier) passe par un service externe de notification.

## Métriques de vérification

- Part des billets dont le nom a changé, avant et après J-7.
- Délai entre « Billets émis » et le billet envoyé.
- Nombre de billets refusés à l'entrée pour un nom incorrect.

## Questions ouvertes

- Quel est le prix du changement de nom après J-7 ?
- Y a-t-il une date après laquelle le changement de nom n'est plus possible du tout ?
- Le Contrôle d'accès doit-il connaître le nouveau nom ? Aucun lien sur la Context Map.
- Un billet mis en revente peut-il encore changer de nom ?

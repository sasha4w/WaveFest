# Bounded Context Canvas : Billetterie

Version formulaire (modèle ddd-crew rempli) : [bc-canvas-billetterie.html](bc-canvas-billetterie.html)

## Nom et objectif

**Billetterie**

Vendre les pass d'un festival par vagues successives (Early, Regular, Last minute) sans jamais dépasser les quotas. Elle réserve les places à la création de la commande, encaisse via le PSP et émet les billets nominatifs une fois le paiement confirmé.

## Classification

| Axe | Choix | Pourquoi |
| --- | --- | --- |
| Type de domaine | **Core** | La vente par vagues avec quotas est ce qui distingue WaveFest. |
| Modèle économique | **Revenue** | C'est le contexte qui encaisse. |
| Évolution | **Custom built** | Les règles de vagues et de réservation sont propres au festival. |

## Rôle

Execution context (moteur) : applique les règles de vente et fait avancer la commande.

## Langage ubiquitaire

| Terme | Définition dans ce contexte |
| --- | --- |
| Pass | Produit vendu (1 jour, 3 jours, VIP), donne droit à un billet. |
| Billet | Titre d'accès nominatif émis après paiement. |
| Vague | Période de vente à prix fixe (Early, Regular, Last minute). |
| Quota | Nombre maximum de pass vendables dans une vague. |
| Grille des prix | Prix des pass pour la vague en cours. |
| Commande | Liste d'achat d'un festivalier, 6 billets maximum. |
| Réservation | Places bloquées pour une commande non payée. |
| Expiration | Fin du délai de 15 min, les places sont libérées. |
| Paiement | Encaissement de la commande par le PSP. |
| Festivalier | Titulaire du compte qui passe la commande. |

## Règles métier (invariants)

- Trois vagues successives, une seule ouverte à la fois, chacune avec son prix, son quota et sa date limite.
- Une vague se clôt à sa date limite ou dès que son quota est épuisé.
- Places vendues + places réservées ne dépassent jamais le quota de la vague.
- 6 billets maximum par commande, au-delà la commande est refusée.
- Les places sont réservées à la création de la commande, avant le paiement.
- Commande non payée sous 15 min : elle expire et ses places sont libérées.
- Paiement refusé : commande annulée, places libérées.
- Aucun billet émis sans paiement confirmé.
- Le prix appliqué est celui de la vague à la création de la commande.
- Chaque billet est nominatif et rattaché au compte du festivalier.

## Entrées

| Type | Message | Émetteur | Relation |
| --- | --- | --- | --- |
| Commande | Acheter un pass | Festivalier | Acteur |
| Commande | Valider la commande | Festivalier | Acteur |
| Commande | Expirer la commande | Politique délai 15 min | Interne |
| Événement | Vagues définies | Programmation | Customer/Supplier (U) |
| Événement | Paiement confirmé / refusé | PSP | ACL (U, externe) |
| Événement | Billet transféré | Revente | Customer/Supplier (D) |
| Requête | Grille des prix | Festivalier | Acteur |
| Requête | Prix d'achat du billet | Revente | Customer/Supplier (D) |
| Requête | Prix payé de la commande | Remboursement billet | Shared Kernel |

## Sorties

| Type | Message | Destinataire | Relation |
| --- | --- | --- | --- |
| Commande | Demander le paiement | PSP | ACL (U, externe) |
| Commande | Notifier la prochaine vague | Notifications | Conformist (U) |
| Événement | Billets émis | Contrôle d'accès | Customer/Supplier (D) |
| Événement | Billets émis | Gestion de billet, Revente | Customer/Supplier (D) |
| Événement | Commande expirée / annulée | Notifications | Conformist (U) |
| Requête | Statut du paiement | PSP | ACL (U, externe) |

Dans la colonne Relation, (U) veut dire que l'autre contexte est en amont, (D) qu'il est en aval.

## Hypothèses

- Le PSP est un système externe, isolé derrière une ACL.
- Les notifications passent par un service d'e-mailing externe dont on adopte le modèle (absent de la Context Map).
- Le quota est défini par vague et par type de pass.
- Changement de nom, revente et remboursement vivent hors de la Billetterie (Gestion de billet, Revente, Remboursement billet).

## Métriques de vérification

- Nombre de surventes (billets émis au-delà du quota) : doit rester à 0.
- Part des commandes expirées ou annulées sur les commandes créées.
- Délai entre paiement confirmé et billets émis.
- Nombre d'évolutions de la Billetterie imposées par un autre contexte (frontière mal placée si élevé).

## Questions ouvertes

- Le quota est-il par type de pass ou global à la vague ?
- Les places invendues d'une vague close par sa date passent-elles à la vague suivante ?
- Paiement confirmé après l'expiration des 15 min : on émet ou on rembourse ?
- La limite de 6 billets vaut-elle par commande ou par compte ?
- Qui émet le billet : la Billetterie ou Gestion de billet ?

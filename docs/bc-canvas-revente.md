# Bounded Context Canvas : Revente

Version formulaire (modèle ddd-crew rempli) : [bc-canvas-revente.html](bc-canvas-revente.html)

## Nom et objectif

**Revente**

Permettre à un festivalier de revendre son billet à un autre sur la plateforme officielle, sans spéculation. Elle plafonne le prix, encaisse l'acheteur, prélève la commission et transfère le billet.

## Classification

| Axe | Choix | Pourquoi |
| --- | --- | --- |
| Type de domaine | **Supporting** | Service qui protège la vente officielle, sans être le coeur de l'offre. |
| Modèle économique | **Revenue** | Chaque revente rapporte 5 % de commission. |
| Évolution | **Custom built** | Le plafond de prix et le transfert sont propres à WaveFest. |

## Rôle

Execution context : mène une revente de l'annonce au transfert.

## Langage ubiquitaire

| Terme | Définition dans ce contexte |
| --- | --- |
| Revente | Vente d'un billet acheté sur l'application, entre deux festivaliers. |
| Annonce | Billet proposé à la revente avec son prix. |
| Vendeur | Festivalier titulaire du billet mis en revente. |
| Acheteur | Festivalier qui achète un billet en revente. |
| Prix d'achat | Prix payé à l'origine pour le billet. |
| Prix plafonné | Prix de revente, au maximum égal au prix d'achat. |
| Commission | 5 % de la transaction prélevés par les organisateurs. |
| Transfert | Passage du billet du vendeur à l'acheteur. |
| Reversement | Paiement au vendeur du prix de revente, commission déduite. |
| Plateforme officielle | Seul endroit où la revente est autorisée. |

## Règles métier (invariants)

- La revente se fait uniquement entre festivaliers, sur la plateforme officielle.
- Le prix de revente ne dépasse jamais le prix d'achat.
- Une commission de 5 % est prélevée sur chaque revente.
- Seul le titulaire d'un billet peut le mettre en revente.
- Un billet ne peut avoir qu'une annonce active à la fois.
- Un billet déjà utilisé à l'entrée ne peut pas être revendu.
- Le transfert n'a lieu qu'après le paiement confirmé de l'acheteur.
- Le vendeur est payé seulement une fois le billet transféré.

## Entrées

| Type | Message | Émetteur | Relation |
| --- | --- | --- | --- |
| Commande | Mettre un billet en revente | Festivalier (vendeur) | Acteur |
| Commande | Acheter un billet en revente | Festivalier (acheteur) | Acteur |
| Commande | Retirer l'annonce | Festivalier (vendeur) | Acteur |
| Événement | Billets émis | Billetterie | Customer/Supplier (U) |
| Événement | Paiement confirmé / refusé | PSP | ACL (U, externe) |
| Requête | Billets en revente | Festivalier | Acteur |

## Sorties

| Type | Message | Destinataire | Relation |
| --- | --- | --- | --- |
| Commande | Demander le paiement | PSP | ACL (U, externe) |
| Commande | Reverser au vendeur | PSP | ACL (U, externe) |
| Événement | Billet transféré | Billetterie | Customer/Supplier (U) |
| Événement | Billet revendu | Notifications | Conformist (U) |
| Requête | Prix d'achat du billet | Billetterie | Customer/Supplier (U) |

Dans la colonne Relation, (U) veut dire que l'autre contexte est en amont, (D) qu'il est en aval.

## Hypothèses

- Sur la Context Map, la Billetterie est en amont de Revente.
- La commission est déduite de ce que reçoit le vendeur.
- Le billet transféré prend le nom de l'acheteur.

## Métriques de vérification

- Part des billets revendus sur les billets émis.
- Nombre d'annonces refusées pour prix supérieur au prix d'achat.
- Délai entre le paiement de l'acheteur et le transfert du billet.

## Questions ouvertes

- La commission de 5 % est-elle payée par le vendeur ou par l'acheteur ?
- Jusqu'à quand peut-on revendre : J-7, la veille, le jour même ?
- Concert annulé après une revente : qui est remboursé, et sur quel prix ?
- Qui prévient Gestion de billet et Contrôle d'accès du nouveau titulaire ? Aucun lien sur la Context Map.
- Peut-on revendre un pass VIP comme les autres ?

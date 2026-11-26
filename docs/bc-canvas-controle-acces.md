# Bounded Context Canvas : Contrôle d'accès

Version formulaire (modèle ddd-crew rempli) : [bc-canvas-controle-acces.html](bc-canvas-controle-acces.html)

## Nom et objectif

**Contrôle d'accès**

Laisser entrer sur le site uniquement les porteurs d'un billet valide, une seule fois. À l'entrée, le billet est scanné puis échangé contre un bracelet RFID.

## Classification

| Axe | Choix | Pourquoi |
| --- | --- | --- |
| Type de domaine | **Supporting** | Protège la recette et la sécurité du site, sans règle de vente. |
| Modèle économique | **Compliance** | Sécurité du site et lutte contre la fraude. |
| Évolution | **Product** | Le scan de billets et les bracelets RFID existent sur étagère. |

## Rôle

Enforcer context : fait respecter à l'entrée ce que les autres contextes ont décidé.

## Langage ubiquitaire

| Terme | Définition dans ce contexte |
| --- | --- |
| Billet | QR à scanner à l'entrée, valide ou non. |
| Scan | Lecture du billet au portique. |
| Portique | Point d'entrée du site où les billets sont contrôlés. |
| Bénévole portique | Personne qui scanne les billets et remet les bracelets. |
| Billet valide | Billet émis, jamais utilisé, pour le bon jour. |
| Billet utilisé | Billet déjà échangé contre un bracelet. |
| Entrée validée | Passage accepté au portique. |
| Entrée refusée | Passage rejeté, avec le motif. |
| Bracelet | Support RFID remis en échange du billet, sert d'accès et de moyen de paiement. |
| Accès VIP | Droit d'entrer au backstage et au bar dédié. |

## Règles métier (invariants)

- Seul un billet émis par la Billetterie peut être valide.
- Un billet ne s'échange qu'une seule fois contre un bracelet.
- Un bracelet est associé à un seul billet.
- Un pass 1 jour ne donne accès qu'à son jour.
- Seul le pass VIP ouvre le backstage et le bar dédié.
- Après l'échange, c'est le bracelet qui donne accès au site.
- Toute entrée refusée indique son motif au bénévole.

## Entrées

| Type | Message | Émetteur | Relation |
| --- | --- | --- | --- |
| Commande | Scanner le billet | Bénévole portique | Acteur |
| Commande | Remettre le bracelet | Bénévole portique | Acteur |
| Commande | Scanner le bracelet | Bénévole portique | Acteur |
| Événement | Billets émis | Billetterie | Customer/Supplier (U) |
| Requête | Nombre d'entrées | Organisation | Acteur |

## Sorties

| Type | Message | Destinataire | Relation |
| --- | --- | --- | --- |
| Événement | Bracelet remis | Paiement cashless | Partnership |
| Événement | Entrée validée | Paiement cashless | Partnership |

Dans la colonne Relation, (U) veut dire que l'autre contexte est en amont, (D) qu'il est en aval.

## Hypothèses

- Les billets valides sont connus à l'avance grâce à « Billets émis », le portique ne dépend pas de la Billetterie le jour J.
- Le bracelet remis à l'entrée est le même que celui du cashless.
- L'identité du titulaire est vérifiée par le bénévole.

## Métriques de vérification

- Temps moyen de passage au portique.
- Nombre d'entrées refusées, par motif.
- Nombre de billets échangés deux fois : doit rester à 0.

## Questions ouvertes

- Le portique doit-il fonctionner sans réseau ?
- Comment le portique apprend-il qu'un billet a été revendu ou a changé de nom ?
- Que fait-on d'un bracelet perdu ou cassé ?
- Le bénévole vérifie-t-il une pièce d'identité ?
- Un pass 3 jours garde-t-il le même bracelet les trois jours ?

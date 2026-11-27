# ADR 001 : Le Remboursement billet est séparé de la Billetterie

- Statut : accepté

## Contexte

Quand une tête d'affiche est annulée, les pass du jour concerné sont remboursés en partie (prix payé / nombre de jours du pass / 2, par jour annulé). Ce calcul part du **prix payé**, qui est connu de la Billetterie : c'est elle qui tient la commande et encaisse via le PSP.

La question s'est posée pendant le découpage : le remboursement est-il la suite de la vente, donc dans la Billetterie, ou un contexte à part ?

Ce qui rapproche les deux :

- Ils parlent de la même commande et du même prix payé.
- Ils passent tous les deux par le PSP.

Ce qui les sépare :

- **Le déclencheur.** La Billetterie réagit au festivalier qui achète. Le remboursement réagit à l'organisation qui annule un concert ; le festivalier ne demande rien.
- **Le moment.** La vente s'arrête quand les vagues sont closes. Une annulation peut arriver des semaines plus tard, jusqu'au jour du festival.
- **Le langage.** Côté Billetterie on parle de vague, quota, réservation, expiration. Côté remboursement on parle de jour annulé, pass concerné, bénéficiaire, échec PSP, nouvelle tentative. Aucun de ces mots n'a de sens de l'autre côté.
- **La raison d'être.** La Billetterie rapporte de l'argent (core, revenue). Le remboursement est une obligation envers le festivalier (supporting, compliance).
- **Le rythme de changement.** Les prix et les quotas changent à chaque édition. La règle de remboursement ne change que si l'organisation revoit sa politique d'annulation.

C'est la frontière la plus discutable de notre Context Map : les deux contextes ont besoin de la même donnée, et le cas du billet revendu avant l'annulation n'est pas encore tranché.

## Options envisagées

### 1. Un seul contexte : le remboursement vit dans la Billetterie

La Billetterie garde la commande du début à la fin, y compris le remboursement.

- Plus : le prix payé est sur place, pas de lien à maintenir, une seule équipe.
- Moins : la Billetterie, notre core, doit connaître les concerts annulés, les relances du PSP et les e-mails d'annulation. Elle dépend alors de la Programmation pour autre chose que les vagues.
- Moins : une modification de la politique d'annulation oblige à toucher au contexte qui vend, en pleine période de vente.

### 2. Deux contextes reliés par un Shared Kernel (commande et prix payé)

Remboursement billet est un contexte à part. Il partage avec la Billetterie un petit modèle commun : la commande et son prix payé.

- Plus : chaque contexte garde son langage et ses règles ; la Billetterie ne sait rien des annulations.
- Plus : une seule définition du prix payé, donc pas d'écart entre ce qui a été encaissé et ce qui sert au calcul.
- Moins : le modèle partagé ne peut plus changer sans l'accord des deux côtés.

### 3. Deux contextes en Customer / Supplier (Billetterie en amont)

Remboursement billet est un contexte à part et garde sa propre copie du prix payé, reçue dans l'événement « Billets émis ».

- Plus : les deux contextes évoluent chacun de leur côté ; le remboursement fonctionne même si la Billetterie est indisponible.
- Moins : le prix payé existe en double, il faut garantir que la copie reste juste (revente, changement de titulaire).
- Moins : la Billetterie doit publier et faire vivre un événement de plus, pour un contexte qui ne sert qu'en cas d'annulation.

## Décision

Option 2 : **Remboursement billet est un contexte séparé, relié à la Billetterie par un Shared Kernel** limité à la commande et au prix payé.

Parce que :

- Le remboursement n'a ni le même déclencheur, ni le même acteur, ni le même vocabulaire que la vente. Le garder dans la Billetterie ferait entrer « concert annulé » et « nouvelle tentative » dans un contexte qui ne doit penser qu'aux vagues et aux quotas.
- Notre core reste concentré sur une seule promesse : vendre sans jamais dépasser le quota.
- Le remboursement doit être juste au centime. Partager la définition du prix payé évite qu'une copie devienne fausse, ce que l'option 3 ne garantit pas.
- C'est la même équipe qui porte les deux contextes aujourd'hui : le coût de coordination du Shared Kernel reste faible.

## Conséquences

Plus simple :

- La Billetterie ne change pas quand la politique d'annulation change.
- Le processus « Annulation d'un concert » se lit d'un seul tenant dans un contexte : calcul, versement, échec PSP, relance, e-mail.
- Le suivi des remboursements (délai, taux d'échec, écart entre calculé et versé) se mesure sans toucher à la vente.

Plus difficile :

- La commande et le prix payé ne peuvent plus être modifiés par la Billetterie seule : chaque changement doit être validé des deux côtés. C'est le compromis que nous acceptons.
- Le Shared Kernel doit rester petit. Si d'autres notions y entrent (titulaire, vague, statut de paiement), les deux contextes redeviennent un seul sans le dire.
- Le billet revendu avant l'annulation reste à trancher : la Billetterie connaît le prix d'achat d'origine, la Revente connaît le prix de revente et le nouveau titulaire. Tant que l'expert métier n'a pas dit qui est remboursé et sur quel prix, le Shared Kernel ne suffit pas pour ce cas.

À revoir si :

- Les deux contextes sont confiés à deux équipes différentes, ou si le modèle partagé grossit : on passera à l'option 3 (Customer / Supplier, Billetterie en amont).

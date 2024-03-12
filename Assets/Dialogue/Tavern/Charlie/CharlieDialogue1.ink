Ta drzost po tolika letech! Nedokážu ho vystát! #speaker:Dior #portrait:dior_neutral #layout:left
Jak říkáš! #speaker:Charlie #portrait:charlie_neutral
Jak si to vůbec představuje? #speaker:Dior #portrait:dior_neutral
Jenom tak, z ničeho nic si přikráčí a tváří se, že se nic nestalo?!
A nemá rád štěňata! #speaker:Charlie #portrait:charlie_neutral
Máš ty ráda štěňata?
+[Ano]
    ->cuties
+[Ne]
    ->cats

=== cuties ===
Jasně, rozkošní chlupáči! #speaker:Rynn #portrait:rynn_neutral
Jsi můj člověk. #speaker:Charlie #portrait:charlie_neutral
->END

=== cats ===
Mám radši kočky. #speaker:Rynn #portrait:rynn_neutral
Ptala jsem se na štěňata.. #speaker:Charlie #portrait:charlie_neutral
    +[Ano]
        ->cuties
    +[Ne]
        Ne. #speaker:Rynn #portrait:rynn_neutral
        ...Jsi si jistá? #speaker:Charlie #portrait:charlie_neutral
        Vlastně jsou docela rozkošný. #speaker:Rynn #portrait:rynn_neutral
        Já vím! Že jo! #speaker:Charlie #portrait:charlie_neutral
        ->END
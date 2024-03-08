EXTERNAL startDice()

Vidíš ho? #layout:left #portrait:anthony_neutral #speaker:Anthony
... #portrait:rynn_neutral #speaker:Rynn
Tohle už je čtvrtá noc po sobě! #portrait:anthony_neutral #speaker:Anthony
Celý dny někde lítá, pak přijde sem a koukej na něj! 
Místo hraní kostek se mnou, spí! 
Pořád jenom spí...
Možná už ho nebaví hrát. #portrait:rynn_neutral #speaker:Rynn
Nesmysl... Ale tebe to určitě bavit bude, že jo? #portrait:anthony_neutral #speaker:Anthony
Nikdy jsem nehrála... #portrait:rynn_neutral #speaker:Rynn
Je to úplně snadný, já hodím, ty hodíš, komu padnou vyšší čísla, ten vyhraje cenu. #portrait:anthony_neutral #speaker:Anthony
Cenu? #portrait:rynn_neutral #speaker:Rynn
No, tak vždycky se o něco hraje, no ne? #portrait:anthony_neutral #speaker:Anthony
O co?  #portrait:rynn_neutral #speaker:Rynn
O tvůj život. #portrait:anthony_dealmaking #speaker:Anthony
???  #portrait:rynn_neutral #speaker:Rynn
Neměj strach, ne o celej, jenom- malinkou část- fakt malou, ani si toho nevšimneš. #portrait:anthony_neutral #speaker:Anthony
Nuže? 
+[Ano]
    ~startDice()
    ->END
+[Ne]
    Ale notak, jednu malou hru. 
    No tak jo. #portrait:rynn_neutral #speaker:Rynn
    ~startDice()
    ->END
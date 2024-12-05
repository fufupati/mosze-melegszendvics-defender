## Áttekintés

Ez a projekt egy Space Invaders-stílusú arcade játék, amelyet Unity-ben fejlesztettünk. A játékosok egy űrhajót irányítanak, hogy lelőjék az ellenséges idegen invázió hullámait, elkerüljék a támadásokat, és a lehető legmagasabb pontszámot érjék el. A projekt jól strukturált mappahierarchiát követ a könnyű fejlesztés és karbantartás érdekében.

## Főbb Jellemzők

- Játékos által irányított ürhajó
- Véletlenszerűen generált ellenséges űrhajók, akadályok
- Űrhajó lövés
- Űrhajó fejlesztések
- Pontszámok
- Bossok
- Zene

## Mappastruktúra 

### Assets

Az összes játékhoz tartozó erőforrás és projektfájl gyökérmappája.

#### Animations
Az összes animációs fájl helye.
Tartalmazza a játékos és az ellenségek animációit (pl. mozgás, robbanások).
Az animációkhoz Animation Controller fájlokat használ.

#### Prefabs
Újrahasználható játékobjektumok tárolója.

#### Scenes
Unity jelenetfájlokat tartalmaz.

#### Scripts
Az összes C# szkriptet tartalmazza a játékmenethez.

#### Sound
A játékhoz tartozó hangfájlokat tartalmazza.

#### Sprites
A játékban használt 2D-s képfájlokat tárolja.

#### ThirdParty
Harmadik féltől származó eszközök és bővítmények helye.

## Hogyan kell játszani?
- Indítsd el a játékot:
    Nyisd meg a MainMenu jelenetet, és nyomj Play-t az Unity szerkesztőjében.
    Alternatívaként építsd le a projektet, és futtasd az önálló verziót.

- Irányítsd az űrhajódat:
    Használd a nyilakat vagy a WASD billentyűket a mozgáshoz.
    Nyomd meg a Space gombot a lövéshez.

- Semmisítsd meg az ellenségeket:
    Lődd le az ellenséges hullámokat, miközben elkerülöd a támadásaikat.
    Gyűjtsd össze az erősítőket, hogy ideiglenes fejlesztéseket szerezz.
   
- Élj túl és gyűjts pontokat:
    Maradj életben a lehető leghosszabb ideig, és érj el minél magasabb pontszámot.
    A játék véget ér, ha az űrhajódat elpusztítják.
    8 másodperc után újrakezdődik a játék.

## Függőségek
  Unity verzió: 2022.3.51f1 vagy újabb.
  
  Külső eszközök a ThirdParty mappában találhatók.

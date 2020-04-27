Faza e parë:

Udhëzimet për ekzekutimin e programit:

Ekzekutimi i programit bëhet prej Command Prompt duke i dhënë vlerat e argumenteve përkatëse, përkatësisht e fillon me ds dhe pastaj kërkesat përkatëse. Varësisht prej vlerave të argumenteve që i jepni, thirret komanda dhe nënkomanda përkatëse. Në qoftë se jepni numër të ndryshëm të argumenteve, atëherë do t'ju paraqiten lajmërime të ndryshme.

Përshkrim i shkurtër për komandat:

Komanda count

Komanda count përdoret për të numëruar elementet e një teksti. Përmes kësaj komande ne mundësuam numërimin e rreshtave të një teksti me ds count lines , numërimin e fjaleve ds count words , numrin e shkronjave në tekstin e dhënë ds count letters <text, dhe simboleve ds count symbols . Gjithashtu komandat e tjera shtesë për numërimin e zanoreve, bashtingëlloreve dhe fjalive. Për këto nënkomanda jemi bazuar në këtë link: https://www.w3resource.com/csharp-exercises/string/csharp-string-exercise-5.php dhe nga detyrat e ndryshme që kemi bërë më herët në gjuhën programuese Java.

Komanda numerical

Përmes kësaj komande është mundësuar zevendësimi i secilës shkronjë me pozitën e tyre në alfabet dhe anasjelltas. Kemi arritur që shkronjën --a-- të zevëndesojmë me numrin 1 b=2,c=3 etj duke u bazuar dhe marrë informacione ne këtë resurs burimor: https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-encoding

Nënkomanda encode

Kodimi është procesi i vendosjes së një sekuence personazhesh (shkronja, numra, pikësim dhe simbole të caktuara) në një format të specializuar për transmetim ose ruajtje efikase. Në këtë rast enkodon tekstin në pozitat alfabetike të shkronjave.

Nënkomanda decode

Dekodimi është procesi i kundërt - shndërrimi i një formati të koduar përsëri në sekuencën origjinale të karaktereve.Ne kete rast dekodon vargun nga shifrat në shkronjat përkatëse.

Opsioni --separator

Me anë të këtij opsioni e specifikojmë karakterin ndarës ky hapësirat e tekstit enkodohen me karakterin e specifikuar.

Komanda rail-fence

Për këtë komandë jemi bazuar në këtë link: https://robinyon.wordpress.com/2011/10/24/route-cipher/. Kjo komandë tekstin e dhënë e enkripton ashtu që transformon plaintextin duke e organizuar në X shirita, të cilat pastaj lexohen majtas- djathtas.

Nënkomanda encrypt

Me anë të kësaj nënkomande bëhet enkriptimi i plaintext-it në ciphertext. Në programin tonë kjo komandë është realizuar përmes një metode që ka dy parametra: plaintext-in dhe numrin e shiritave.

Nënkomanda decrypt

Me anë të kësaj nënkomande bëhet dekriptimi i ciphertext-it në plaintext. Në programin tonë kjo komandë është realizuar përmes një metode që ka dy parametra: ciphertext-in dhe numrin e shiritave.

Opsioni --show

Ky opsion paraqet tekstin e organizuar në shirita sipas së cilës bëhet edhe enkriptimi i tekstit. Për këtë jemi bazuar në linkun më lart.

Shembujt e ekzekutimit i gjeni tek folderi me emrin Shembuj Ekzekutimi.





Faza e dytë

Udhëzimet për ekzekutimin e programit

Pasi programi është i njëjtë, pra vazhdim i programit nga faza e parë, ekzekutimi bëhet nga Command Prompt duke i dhënë argumentet për komandat përkatëse.
Nëse jepni argumentet jovalide ju njoftoheni nga programi që nuk i keni dhënë argumentet si duhet ose nëse jepni ndonjë komandë që nuk ekziston gjithashtu do të njoftoheni se ajo komandë nuk ekziston.
Kur hapet Command Prompt pra së pari shkruhet komanda pastaj argumentet përkatëse që do t'i tregojmë si më poshtë.

Komanda create-user

Kjo komandë gjeneron një cift të celësave (publik dhe privat) të RSA dhe i ruan me një folder përkatës, në rastin tonë në folderin "keys".
Sintaksa: ds create-user <name>
name paraqet emrin me të cilin dëshirojmë të ruajmë ciftin e celësave të gjeneruar. Emri nuk guxon të përmbajë dicka tjetër përpos numrave, shkronjave dhe simbolit _.
Madhësia e celësit është 2048. Nëse tentojmë të gjenerojmë një celës tjetër me po të njëjtin emër, atëherë programi nuk na lejon sepse ai ekziston paraprakisht.

Komanda delete-user

Kjo komandë fshin të gjithë celësat ekzistues të shfrytëzuesit të cilin ne ia shkruajmë emrin. 
Sintaksa: ds delete-user <name>
Nëse dëshirojmë të fshijmë përdoruesin i cili nuk ekziston, atëherë na paraqitet një mesazh gabimi.
  
Komanda import-key

Kjo komandë ka për qëllim të importojë celësin publik ose privat të shfrytëzuesit nga shtegu që kemi dhënë pra shtegut <path> dhe i vëndos ata në folderin "keys" në këtë rast.
Sintaksa: ds import-key <name> <path>
<name> e përcakton emrin e çelësit që do të ruhet në direktoriumin keys.
<path> e përcakton shtegun e çelësit që do të importohet.
CelArgumenti <name> e përcakton emrin e çelësit që do të ruhet në direktoriumin keys.
Argumenti <path> e përcakton shtegun e çelësit që do të importohet.
Celësi mundët më qenë publik ose privat dhe kemi mundesuar qe programi automatikisht të kupton se qfarë lloj i celësit është duke e shikuar përmbajtjen e fajllit që importohet.Nëse celësi që importohet  është privat automatikish gjenerohet edhe celvsi publik në menyrë që të dytë te ruhen në "keys".  
  
  

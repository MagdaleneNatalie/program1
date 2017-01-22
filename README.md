# TicTacToe TCP/IP

Projekt składa się z 4 bibliotek.

+ TicTacToeClient - jest to program konsolowy, który jest klientem serwera TicTacToeServer. 
+ TicTacToeServer - jest to program konsolowy, któey pełni role serwera. W tym programie jest uruchomiona gra i tam działają wszystkie algorytmy dla gry.
+ TicTacToeGame - jest to biblioteka do gry w kołko i krzyżyk. W skórcie umożliwia stworzenie gry i wykonywanie ruchów i podgląd aktualnego stanu.
+ TicTacToeTests - jest do biblioteka do tesów biblioteki TicTacToeGame. Biblioteka używa zew. biblioteki xUnit. Testy te sprawdzają czy TicTacToeGame działa jak powinno.


## Szybki start

Aby uruchomić grę, należy oczywiście projekt skompilować a następnie uruchomić w następujący sposób:

+ Z folderu bin w katalogu TicTacToeServer uruchomiamy TicTacToeServer.exe. Następnie wpisujemy nick gracza, który będzie pełnił role serwera.
+ Z folderu bin w katalogu TicTacToeClient uruchamiamy TicTacToeClient.exe. To będzie przeciwnik. Wpiosujemy nick gracza i zaczynamy grę. Nalezy postępować zgodnie z tym schematem.




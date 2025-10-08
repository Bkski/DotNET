***** Lab 1 *****
W jednym projekcie mamy 2 rozwiązania i to które wykonujemy zmieniamy naciskając te strzałki w dół:

<img width="200" height="37" alt="image" src="https://github.com/user-attachments/assets/d872a84a-c0da-405c-aa7d-c3ffb9d18f3a" />

Zapyta o sposób w jaki posortowałeś wyniki w 1.4-1.6, linia 47 w program.cs i jest to sortowanie in place, wyniki zapisują się w Lab1\Lab1-14-16\bin\Debug\net8.0\highscores.json


***** Lab 3 *****

W Laboratorium 3 będzie chciał zobaczyć stronę http://localhost:5020/Home/Index oraz http://localhost:5020/Home/Index2
W Index2 dodać należy użytkownika i pokazać że można skasować, sprawdzić czy skasowanie ID którego nie ma poprawnie wyrzuci 404 http://localhost:5020/Home/Delete/999

***** Lab 5 *****

Pewnie będzie chciał sprawdzić czy gwiazdki poprawnie działają(ocena od 1 do 5) i w details czy sie poprawnie wyświetla filmik


***** Lab 7 *****

Chuj jeden wie co tam sie dzieje, hasło dla każdego usera to "P@ssw0rd" i jest domyślne a email można wziąć z indexu, może sprawdzać czy My Orders pojawi się jak użytkownik nie jest zalogowany i czy może wejść na zamówienie innego użytkownika przez link np: http://localhost:5202/orders/77


***** Lab 8 *****

Jakieś lisy ale nie podał strony html więc działają tylko requesty, najlepiej puszczać z http://localhost:5250/swagger/index.html

Strona lisów jest pod:

http://localhost:5250/

Przykładowe dodanie lisa curlem

curl -X POST http://localhost:5250/api/fox -H "Content-Type: application/json" -H "Authorization: Basic dXNlcjpwYXNzd29yZA==" -d "{\"name\":\"Foxy\",\"loves\":5,\"hates\":2}"

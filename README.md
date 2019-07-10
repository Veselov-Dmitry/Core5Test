[![N|Solid](https://raw.githubusercontent.com/Veselov-Dmitry/Core5Test/master/Image.png)]

# Тестовое задание

Необходимо разработать 2D игру:

  - Стартовый экран с 2 кнопками:  “Начать игру” и “Лидерборд”
  - Core-механика игры: 
  - - Вид сверху
  - - Управляем героем по клику он идет и поворачивается  куда кликнули со скоростью Х
  - - Рандомно генерируются враги со скоростью Х врагов в Н секунд. Враги преследуют игрока и надо от них убегать. Когда враг касается героя - игра окончена. Скорость новых врагов со временем растет с параметром Х
 - - Каждые Х секунд игрок получает У очков. 
 - Рандомно генерируются 3 бонуса разных типов - которые ускоряют игрока, дают х10 очков и бессмертие (при касании врага в течении действия бонуса враг погибает)
 - После того как игра окончена очки игрока сохраняются (локально) в лидерборд с возможностью  набрать имя из 5ти букв (по типу аркадных автоматов). После этого появляется экран с возможностью переиграть или выйти в главное меню
 - Лидерборд представляет из себя скролл сохраненных очков с именами и номерами позиции
 - Интерфейс в Игре: отображается время в игре со старта и очки. Контроль представляют из себя иконку и числовое значение

Дополнительные условия:

 - В качестве графических ассетов можно использовать просто квадратный спрайт
 - UI должен поддерживать разные разрешения:iPhone, iPhoneX (c учетом display cutouts), iPad
 - Указанные параметры должны быть настраиваемые, c использованием ScriptableObject
 - Сделать в Unity 2019.1.3
 - Код стайл стандартный майкрософтовский для C#
 - Готовое решение оформить в GitHub, Bitbucket или аналоги

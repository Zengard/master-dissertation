Многоканальная диалоговая система для группы неигровых персонажей с возможностью прерывания и переключения между участниками диалога без потери информативности

Данная система создана в рамках выпускной квалификационной работы. Представляет из себя алгоритм взаимодействия с персонажами с возможностью перывания их диалога по ходу общения. Прерывания построены на различных личностных характеристик каждого из персонажей.

Что нужно для запуска:

1) Unity 2021.3.11f1

2) Visual studio (не ниже 2019 версии)

Запуск проекта происходит через Unity hub.
После запуска должна загрузиться сцена, представленная на скриншоте ниже:

![image](https://github.com/Zengard/master-dissertation/assets/44618246/5a38ef74-5b80-4587-8e3c-e979544de6ab)


Собранный проект представляет из себя сцену с расположенной на ней игроком и неигровыми персонажами. Игрок представлен бобом белого цвета, в то время как неигровые персонажи всеми остальными цветами.
Для запуска игровой сцены необходимо нажать на на кнопку "Play" по центру верхней части экрана.

Как играть:

Передвижение персонажа происходит с помощью клаавиш w, a, s, d

Для начала общения игроку нужно подойти к любому из персонажей, где над ним появится изображение кнопки, нужной для старта диалога

В данном случае это английская кнопка "X" на клавиатуре

Для прокрутки скорости диалога можно нажимать "Х" в процессе общения, тогда отображение текста будет происходить быстрее

Персонажи будут перебивать общение на основе заданных тем и харктеристик, как только они найдут для себя интересное слово, то это слово высветится над головой этого персонажа

Понять то, какой именно в данный момент говорит персонаж можно по иконке облака над головой активного участника, имени, отображенного над диалоговым окном или по диалоговому окну, цвет которого соответствует цвету активного в данный момент персонажа

Настройка проекта:

Для того, чтобы добавить тему для диалога одному из персонажей, нужно создать новый файл диалога из базы данных. Для этого нужно создать создать файл в папке "DialoguesDataBase" представленный на скриншоте ниже 

![image](https://github.com/Zengard/master-dissertation/assets/44618246/be26b593-58ee-4bcc-9701-67a9143bfc1e)

После того, как файл будет создан, он будет выглядеть следующим образом: 

![image](https://github.com/Zengard/master-dissertation/assets/44618246/d33733c0-637b-4611-aa28-04a5a9b5a7aa)

Нужно заполнить поля:
1) Темы
2)  Айди номера
3)  закрепленной характеристики(fixed tratit)
 
В поле тегов нужно указать слова, по которым будет находиться точка интереса. Чем больше слов, тем больше шансов найти пересечение.  В поле Dialogue lines нужно написать текст, который будет отображаться во время диалога 

Для того, чтобы добавить новую тему в список персонажа, нужно выбрать на сцене неигрового персонажа и добавить в поле "List of dialogues" новый элемент. Оформление этого поля показано на скриншоте ниже:

![image](https://github.com/Zengard/master-dissertation/assets/44618246/b7e7f880-ce35-478d-9413-b406748b7c3a)

Нового персонажа можно добавить из папки "Prefabs" -> NPC. Нужно перетащить объет "NPC" на игровую сцену.

Для каждого нового персонажа нужно заполнить поле "Npc Name" а так же настроить значение полей "Personalirt traits" и "List of dialogues". Значения поля "Personalirt traits" определяют, как именно себя будет вести персонаж на сцене при перебивании.
Пример заполненного поля представлен на скриншоте ниже:

![image](https://github.com/Zengard/master-dissertation/assets/44618246/5c99b3c7-c574-4b09-a490-d3fd798a72bf)

Есть возможность запустить проект на двух языках: русском и английском.

Для этого нужно среди всего списка папок найти папку с названием "Scenes", открыть её и выбрать сцену для загрузки: "SceneRussian" для русского языка или "SceneEnglish" для английского соответственно. На скриншоте ниже показано, как выглядят эти сцены в папке:

![image](https://github.com/Zengard/master-dissertation/assets/44618246/09a3b6d3-ac81-4fa2-b1b4-d3253ca51986)






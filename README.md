# Знакомство с 2D игрой на Untity

Итак, что же я узнал из этого урока:
* Научился запускать юнити.
* Познакомился с интерфейсом программы.
* Научился пользоваться магазином ассетов в Unity.
* Научился создавать анимацию по ассетам.
* Научился добавлять простейсую физику для объектов (Box Collider & rigid body 2D).
* Нучился создавать сетку и таймлам.
* Научился создавать палитру для тайлов, а также настраивать масштаб для тайлов.
* Научился использовать палитру тайлов для "рисования" уровня.

Урок 2:
* Научился делать коллайдер для тайла.
* Повторил создание анимаций.
* +- разобрался с привязкой камеры к фону.

Урок 3 *Передвижение*:
* Научился добавлять скрипты для объектов.
* Узнал, что для объекта можно добавить несколько коллайдеров.
* Узнал как читать ввод пользователья с клавиатуры.
* Узнал как менять клавиши управления.
* Узнал как делать привязку камеры к игроку с помощью тега.
* Разобрался с событиями c#.
* Разобрался с новой системой пользовательского ввода значений.

Урок 4 **:
TODO

Урок 5 *Пользовательский интерфейс*:
* Ну тут типа что я сделал, если я не забыл это изменить

# Events
В Unity существуют предопределенные события, которые вызываются в разные моменты выполнения скрипта. Вот некоторые из них:
* Awake() -- вызывается один раз в самом начале выполнения скрипта (когда активный GameObject инициализируется при загрузке сцены, либо если неактивный GO становится активным, либо после инциализации GO, посзданного с помощью Object.Instatiate). Можно использовать в качетсве конструктора класса.
* OnEnable() -- вызвается сразу после того, как объект становится включенным и активным. Также выполняется всякий раз при заходе в Play mode.
* Reset() -- вызывается всякий раз при нажатии кнопки в инспекторе, либо при добавлении нового объекта. Вызывается только в editor mode. Сбрасывает значения до значений по-умолчанию. Используется для сброса значений в инспекторе до нужных. 
* Start() -- вывзается один раз до выполнения всех Update функций. Можно использовать, если объекту нужно ссылаться на какой-то объект (который уже был создан в Awake()).
* FixedUpdate() -- не зависит от частоты кадров. Применяется для вычисления физики. Количество вызовов по умолчанию: 50 в секунду (каждые 0.02 секунды). Для доступа к значению времени используется Time.fixedDeltaTime.
* Update -- вызывается в каждом кадре. Чтобы получить время, прошедшее с момента последнего вызова Update(), используется Time.deltaTime.
* LateUpdate -- вызывается в каждом кадре, но после выполнения Update(). Можно использовать для определения положения камеры сразу после того, как персонаж закончил движение. Чтобы получить время, прошедшее с момента последнего вызова LateUpdate(), используется Time.deltaTime.
* OnGUI() -- вызывается во врем обработки событий графического интерфейса. 
* OnDisable() -- вызывается, когда объект уничтожается. Метод может быть использован для уничтожения переменных.

# Canvas
* Canvas -- GO, в котором должны располагаться все элементы UI.
* Есть три режима рендеринга:
    1. Оверлей - элементы размещаются на экране поверх сцены. 
    2. Рендер камерой - canvas размещается на заданном положении перед камерой и его вид зависит от настроек камеры.
    3. Расположение UI в мире игры - canvas будет считаться объектом сцены. По сути диегетический интерфейс.
* Существуют якоря, которые позволяют привызывать элементы UI к разным точкам канваса.
* Компоненты взаимодействия:
    * Кнопка. Обработчик - OnClick;
    * Флажок. Обработчик - OnValueChanged; Если объединить несколько в группу, то станут взаимоисключающими.
    * Ползунок. Обработчик - OnValueChanged;
    * Полоса прокрутки. Обработчик - OnValueChanged;
    * Выпадающий список. Обработчик - OnValueChanged;
    * Поле ввода. Обработчки - OnValueChange и EndEdit;
    * Скрол. Обработчик - OnValueChanged.
* 
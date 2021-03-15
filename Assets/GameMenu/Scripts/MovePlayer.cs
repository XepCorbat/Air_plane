using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private GameObject player; //Переменна объекта персонажа с которым будем работать.

    public static int speed = 6; //Скорость перемещения персонажа. Запись public static обозначает что мы сможем обращаться к этой переменной из любого скрипта
    public static int _speed; //постоянная скорость перемещения персонажа
    public int rotation = 250; //Скорость пповорота персонажа
    public int jump = 3; //Высота прыжка


    public static bool IsDrawWeapon; //Двоичная переменная, которая будет отвечать достануто ли у нас оружие.  
    public static float x = 0.0f; //угол поворота персонажа по оси x
    void Start()
    {
        IsDrawWeapon = false; //По умолчанию оружие у нас спрятано.
        _speed = speed; //Задаем постоянное стандартное значение скорости персонажа
        player = (GameObject)this.gameObject; //Задаем что наш персонаж это объект на котором расположен скрипт
    }

    void Update()
    {
        if (IsDrawWeapon == true) //Если оружие вытащено
        {
            speed = _speed * 2; // Меняем скорость передвижени(я это сделал потому что, у этой моделки нету анимаций движения простым шагом с мечом. а понижать скорость анимации у бега получиться не красиво)
            if (Input.GetKey(KeyCode.W)) //Если нажать W
            {
                player.transform.position += player.transform.forward * speed * Time.deltaTime; //Перемещаем персонажа в перед, с заданой скорость. Time.deltaTime ставится для плавного перемещения персонажа, если этого не будет он будет двигаться рывками
            }
            if (Input.GetKey(KeyCode.S))
            {
                speed = _speed / 2; //При передвижениии назад снижаем скорость перемещения
                player.transform.position -= player.transform.forward * speed * Time.deltaTime; //Перемещаем назад
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                speed = _speed * 2; //Возвращаем cтандартное значение
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.transform.position -= player.transform.right * speed * Time.deltaTime; //перемещаем в лево
            }
            if (Input.GetKey(KeyCode.D))
            {
                player.transform.position += player.transform.right * speed * Time.deltaTime; //перемещаем в право
            }
            if (Input.GetKey(KeyCode.Space))
            {
                player.transform.position += player.transform.up * jump * Time.deltaTime; //Прыгаем
            }

            if (Input.GetKey(KeyCode.Tab)) //При нажатии и на кнопку Tab
            {
                IsDrawWeapon = false; //Мы спрячем наше оружие.
            }
        }
        else if (IsDrawWeapon == false) //Если оружие не спрятано. |||||| Сделано разделение на движения в зависимости от того вытащено ли у нас оружие или нет, потому что персонаж будет перемещаться сразной скорость у меня в этих случаях, как я уже написал из за отсутсвия некоторых анимаций у модельки.  
        {
            speed = _speed;//Скорость в стандартное значение
            if (Input.GetKey(KeyCode.LeftShift)) //Если зажать левый Shift
            {
                speed = _speed * 2; //Увеличиваем скорость перемещения(бег)
            }
            if (Input.GetKeyUp(KeyCode.LeftShift)) //Если отпустить
            {
                speed = _speed; //Возвращаем стандартное значение
            }
            if (Input.GetKey(KeyCode.W)) //Если нажать W
            {
                player.transform.position += player.transform.forward * speed * Time.deltaTime; //Перемещаем персонажа в перед.
            }
            if (Input.GetKey(KeyCode.S))
            {
                speed = _speed / 2;
                player.transform.position -= player.transform.forward * speed * Time.deltaTime; //Перемещаем назад
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                speed = _speed; //Возвращаем cтандартное значение
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.transform.position -= player.transform.right * speed * Time.deltaTime; //перемещаем в лево
            }
            if (Input.GetKey(KeyCode.D))
            {
                player.transform.position += player.transform.right * speed * Time.deltaTime; //перемещаем в право
            }
            if (Input.GetKey(KeyCode.Space))
            {
                player.transform.position += player.transform.up * jump * Time.deltaTime; //Прыгаем
            }
            if (Input.GetKey(KeyCode.Tab)) //при нажатии на кнопку таб
            {
                IsDrawWeapon = true; //Мы вытащим наше оружие
            }
        }

        //Поворачиваем персонажа. Так как наша переменная x глобальна, из скрипта камеры в неё будем записывать длину на сколько сместился указатель мыши и по оси X и относительно этого будет повернут наш персонаж
        Quaternion rotate = Quaternion.Euler(0, x, 0); //Создаем новую переменную типа Quaternion для задавания угла поворота
        player.transform.rotation = rotate; //Поворачиваем персонаж

    }
}

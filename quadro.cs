
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class quadro : MonoBehaviour
{
    private const float MAXBOOST = 20;

    private double roll; //крен
    //в данном случае, так как объект 2д пространства, останется один угол
    public double boost; //газ, подъем вверх-вниз

   // public double tagretRollDegrees;
   public double targetRoll; 



    //PID регуляторы, которые будут стабилизировать углы
    private PID rollPID = new PID(100, 0, 100);
    private PID highPID = new PID(100,0,100);

    void ReadRotation()
    {

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! vector2
        Vector3 rot = GameObject.Find("DRONE").GetComponent<Transform>().rotation.eulerAngles;
        roll = rot.z;
    }

    void GetHigh()
    {

    }

    void Stabilize()
    {
        double dRoll = targetRoll - roll;

        dRoll -= Math.Ceiling(Math.Floor(dRoll / 180.0) / 2.0) * 360;

        double motor1 = boost;
        double motor2 = boost;


        double powerLimit = (boost > MAXBOOST) ? MAXBOOST : boost;

        //управление креном:

        double rollForce = -rollPID.calc(0, dRoll / 180.0);
        rollForce = rollForce > powerLimit ? powerLimit : rollForce;
        rollForce = rollForce < -powerLimit ? -powerLimit : rollForce;
        motor1 += rollForce;
        motor2 += -rollForce;

        //возможно создать класс drone внутри quadro
        GameObject.Find("drone1").GetComponent<Motors>().power = (float)motor1;
        GameObject.Find("drone2").GetComponent<Motors>().power = (float)motor2;
    }


    void FixedUpdate()
    {
        ReadRotation();
        Stabilize();
    }

    public class PID
    {

        private double P;
        private double I;
        private double D;

        private double prevErr;
        private double sumErr;

        public PID(double P, double I, double D)
        {
            this.P = P;
            this.I = I;
            this.D = D;
        }

        public double calc(double current, double target)
        {

            double dt = Time.fixedDeltaTime;

            double err = target - current;
            this.sumErr += err;

            double force = this.P * err + this.I * this.sumErr * dt + this.D * (err - this.prevErr) / dt;

            this.prevErr = err;
            return force;
        }


    }


}


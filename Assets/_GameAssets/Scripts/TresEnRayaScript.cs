using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TresEnRayaScript : MonoBehaviour {
    public Camera camara;
    public GameObject prefabFichaPlayer;
    public GameObject prefabFichaRival;
    public Text textoGanador;

    bool turnoPlayer = true;
    int[] celdas = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    int numTurnos = 1;
    /*void Update () {
        if (numTurnos > 9) {
            return;
        }
        if (turnoPlayer && Input.GetMouseButtonDown(0)) {
            Ray ray = camara.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch)) {
                int indice = int.Parse(rch.transform.gameObject.name.Substring(5, 1));
                if (celdas[indice] != -1) {
                    return;
                }
                //Generamos la ficha
                GeneracionFichaYCambioDeTurno(rch, indice);
            }
        }
    }*/
    private void Update() {
        if (numTurnos > 9) {
            return;
        }
        if (turnoPlayer && (Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) {
            Ray ray = camara.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch)) {
                int indice = int.Parse(rch.transform.gameObject.name.Substring(5, 1));
                if (celdas[indice] != -1) {
                    return;
                }
                //Generamos la ficha
                GeneracionFichaYCambioDeTurno(rch, indice);
            }
        }
    }


    private void GeneracionFichaYCambioDeTurno(RaycastHit rch, int indice) {
        GameObject ficha = Instantiate(prefabFichaPlayer, rch.transform);
        ficha.transform.Translate(Vector3.up * 0.1f);
        celdas[indice] = 0;
        turnoPlayer = false;
        numTurnos++;
        ComprobarTresEnRaya();
        if (numTurnos < 9) {
            Invoke("TurnoRival", 1);
        }
    }

    void TurnoRival() {
        int pos;
        do {
            pos = Random.Range(0, 8);
        } while (celdas[pos] != -1);
        GameObject casilla = GameObject.Find("Celda" + pos);
        GameObject ficha = Instantiate(prefabFichaRival, casilla.transform);
        ficha.transform.Translate(Vector3.up * 0.1f);
        celdas[pos] = 1;
        turnoPlayer = true;
        numTurnos++;
        ComprobarTresEnRaya();
    }

    void ComprobarTresEnRaya() {
        bool hayTresEnRaya = false;
        int ganador = -1;
        if (celdas[0]!=-1 && celdas[0]==celdas[1] && celdas[0] == celdas[2]) {
            hayTresEnRaya = true;
            ganador = celdas[0];
        } else if (celdas[3] != -1 && celdas[3] == celdas[4] && celdas[3] == celdas[5]) {
            hayTresEnRaya = true;
            ganador = celdas[3];
        } else if (celdas[6] != -1 && celdas[6] == celdas[7] && celdas[6] == celdas[8]) {
            hayTresEnRaya = true;
            ganador = celdas[6];
        } else if (celdas[0] != -1 && celdas[0] == celdas[3] && celdas[0] == celdas[6]) {
            hayTresEnRaya = true;
            ganador = celdas[0];
        } else if (celdas[1] != -1 && celdas[1] == celdas[4] && celdas[1] == celdas[7]) {
            hayTresEnRaya = true;
            ganador = celdas[1];
        } else if (celdas[2] != -1 && celdas[2] == celdas[5] && celdas[2] == celdas[8]) {
            hayTresEnRaya = true;
            ganador = celdas[2];
        } else if (celdas[0] != -1 && celdas[0] == celdas[4] && celdas[0] == celdas[8]) {
            hayTresEnRaya = true;
            ganador = celdas[0];
        } else if (celdas[2] != -1 && celdas[2] == celdas[4] && celdas[2] == celdas[6]) {
            hayTresEnRaya = true;
            ganador = celdas[2];
        } 
        if (hayTresEnRaya) {
            if (ganador==0) {
                textoGanador.text = "GANA JUGADOR";
            } else {
                textoGanador.text = "GANA AI";
            }
        } else if (numTurnos>9){
            textoGanador.text = "TABLAS";
        }
    }
}

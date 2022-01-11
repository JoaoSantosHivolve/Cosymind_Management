using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo
{
    public string id;
    public string clientName;
    public string phoneNumber;
    public string address;
    public string email;
    public string nif;
    public string observations;

    public CellInfo(string id, string clientName, string phoneNumber, string address, string email, string nif, string observations)
    {
        this.id = id;
        this.clientName = clientName;
        this.phoneNumber = phoneNumber;
        this.address = address;
        this.email = email;
        this.nif = nif;
        this.observations = observations;
    }
}
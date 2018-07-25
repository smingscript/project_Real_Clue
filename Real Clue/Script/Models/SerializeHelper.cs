using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializeHelper
{
    public static void Register()
    {
        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(List<int>), (byte)'C', SerializePlayerCards, DeserializePlayerCards);
        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(PhotonPlayer), (byte)'P', SerializePhotonplayer, DeserializePhotonplayer);
        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(Dictionary<PhotonPlayer, List<int>>), (byte)'D', SerializeDictionary, DeserializeDictionary);
    }

    public static byte[] SerializePlayerCards(object obj)
    {
        List<int> playerCards = (List<int>)obj;

        var bytes = new byte[playerCards.Count * sizeof(int)];
        int index = 0;
        foreach (int cardId in playerCards)
        {
            ExitGames.Client.Photon.Protocol.Serialize(cardId, bytes, ref index);
        }

        return bytes;
    }

    // Convert a byte array to an Object
    public static object DeserializePlayerCards(byte[] arrBytes)
    {
        List<int> playerCards = new List<int>();
        int index = 0;
        foreach (byte cardIdByte in arrBytes)
        {
            int cardId;
            ExitGames.Client.Photon.Protocol.Deserialize(out cardId, arrBytes, ref index);
            playerCards.Add(cardId);
        }

        return playerCards;
    }

    public static byte[] SerializePhotonplayer(object obj)
    {
        PhotonPlayer player = (PhotonPlayer)obj;

        return ExitGames.Client.Photon.Protocol.Serialize(player);
    }

    // Convert a byte array to an Object
    public static object DeserializePhotonplayer(byte[] arrBytes)
    {
        return ExitGames.Client.Photon.Protocol.Deserialize(arrBytes);
    }

    public static byte[] SerializeDictionary(object obj)
    {
        Dictionary<PhotonPlayer, List<int>> player = obj as Dictionary<PhotonPlayer, List<int>>;

        return ExitGames.Client.Photon.Protocol.Serialize(player);
    }

    // Convert a byte array to an Object
    public static Dictionary<PhotonPlayer, List<int>> DeserializeDictionary(byte[] arrBytes)
    {
        return ExitGames.Client.Photon.Protocol.Deserialize(arrBytes) as Dictionary<PhotonPlayer, List<int>>;
    }

    #region Using BinaryFormatter
    //public static byte[] SerializePlayerCards(object obj)
    //{
    //    if (obj == null)
    //        return null;

    //    BinaryFormatter bf = new BinaryFormatter();
    //    MemoryStream ms = new MemoryStream();
    //    bf.Serialize(ms, obj);

    //    return ms.ToArray();

    //}

    //// Convert a byte array to an Object
    //public static object DeserializePlayerCards(byte[] arrBytes)
    //{
    //    MemoryStream memStream = new MemoryStream();
    //    BinaryFormatter binForm = new BinaryFormatter();
    //    memStream.Write(arrBytes, 0, arrBytes.Length);
    //    memStream.Seek(0, SeekOrigin.Begin);
    //    Object obj = (Object)binForm.Deserialize(memStream);

    //    return obj;
    //}
    #endregion
}

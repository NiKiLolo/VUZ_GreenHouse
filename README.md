# Поддержка UPC UA
Для нормально привязки вашего UPC UA требуется поменять ApplicationUri на тот который прописан в вашем OPC UA servers (пример на фото)

<img width="1407" height="629" alt="image" src="https://github.com/user-attachments/assets/cf04bcb2-9a7d-4d3c-932a-83fc5a53cff5" />


```
 var applicationDescription = new ApplicationDescription
 {
     ApplicationName = "AdaptiveControlSystem Client",
     ApplicationUri = "urn:DESKTOP-9KRMEGG:OPCUA:SimulationServer",
     ApplicationType = ApplicationType.Client
 };
```

export class menuDTO{
    menuName : string
    actions: actionDTO[];
}
export class actionDTO  {
    actionType : string
    httpType : string 
    definition : string
    code : string
}

// ActionDTO
// public string ActionType { get; set; }
// public string HttpType { get; set; }
// public string Definiton { get; set; }
// public string Code { get; set; }

// public class MenuDTO
// {
//     public string MenuName { get; set; }
//     public List<ActionDTO> Actions { get; set; } = new();
// }
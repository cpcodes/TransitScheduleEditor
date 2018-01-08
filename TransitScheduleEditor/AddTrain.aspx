<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTrain.aspx.cs" Inherits="TransitScheduleEditor.AddTrain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" />
            <asp:FormView runat="server" ID="addTrainForm"
                ItemType="TransitScheduleEditor.Models.StaticTrain"
                InsertMethod="InsertTrain" DefaultMode="Insert"
                RenderOuterTable="false" OnItemInserted="addTrainForm_ItemInserted"
                OnCallingDataMethods="addTrainForm_CallingDataMethods">
                <InsertItemTemplate>
                    <fieldset>
                        <ol>
                            <asp:DynamicEntity runat="server" Mode="Insert" />
                        </ol>
                        <asp:Button runat="server" Text="Insert" CommandName="Insert" />
                        <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="cancelButton_Click" />
                    </fieldset>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </form>
</body>
</html>

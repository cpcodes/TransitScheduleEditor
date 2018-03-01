<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTrain.aspx.cs" Inherits="TransitScheduleEditor.AddTrain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        #inputTable tr > td:first-child {
            text-align: right;
            background-color:cadetblue;
        }
    </style>
    <title>Add Train to Schedule</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" />
            <asp:DynamicDataManager runat="server" ID="TrainInsertDDM" AutoLoadForeignKeys="false" >
                <DataControls>
                    <asp:DataControlReference ControlID="addTrainForm" />
                </DataControls>
            </asp:DynamicDataManager>

            <asp:FormView runat="server" ID="addTrainForm"
                ItemType="TransitScheduleEditor.Models.StaticTrain"
                InsertMethod="InsertTrain" DefaultMode="Insert"
                RenderOuterTable="false" OnItemInserted="addTrainForm_ItemInserted"
                OnCallingDataMethods="addTrainForm_CallingDataMethods">
                <InsertItemTemplate>
                    <fieldset>
                        <table id="inputTable">
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Departure: " /></td>
                                <td>
                                    <asp:DynamicControl runat="server" DataField="Departure" Mode="Insert" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Train: " /></td>
                                <td>
                                    <asp:DynamicControl runat="server" DataField="TrainName" Mode="Insert" />

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Number: " /></td>
                                <td>
                                    <asp:DynamicControl runat="server" DataField="Number" Mode="Insert" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Direction: " /></td>
                                <td>
                                    <asp:DynamicControl runat="server" DataField="DirectionName" Mode="Insert" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Platform: " /></td>
                                <td>
                                    <asp:DynamicControl runat="server" DataField="PlatformName" Mode="Insert" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Days: " /></td>
                                <td>
                                    <asp:DynamicControl runat="server" DataField="DayName" Mode="Insert" />
                                </td>
                            </tr>
                        </table>
                        <asp:Button runat="server" Text="Insert" CommandName="Insert" />
                        <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="cancelButton_Click" />
                    </fieldset>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </form>
</body>
</html>

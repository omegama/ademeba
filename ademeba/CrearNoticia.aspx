<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearNoticia.aspx.cs" Inherits="ademeba.CrearNoticia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" ID="addNewImage" Text="Nueva Imagen" OnClick="newImage" />
        <div>
            <asp:Table runat="server" ID="createIdTable"></asp:Table>
        </div>
        <asp:Button runat="server" Text="Guardar" ID="saveBtn" OnClick="saveNew" />
        <div>
            <asp:Table runat="server" ID="newsTable">
                
            </asp:Table>
        </div>
    </form>
</body>
</html>

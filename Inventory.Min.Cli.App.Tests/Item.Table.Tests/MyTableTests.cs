using CLIHelper;
using Inventory.Min.Cli.App.TestApi;
using Inventory.Min.Data;
using Serilog.Sinks.InMemory.Assertions;
using Xunit;
using XUnit.Helper;
using dataUtil = Inventory.Min.Cli.App.Tests.ItemTests.TestUtil;

namespace Inventory.Min.Cli.App.Tests.ItemTests;

[Collection(DbTests)]
[TestCaseOrderer(OrdererTypeName, OrdererAssemblyName)]
public class MyTableTests
    : OrderTest
        , IClassFixture<InventoryMyTablesFixture>
{
  private InventoryMyTablesFixture fixture;

  public MyTableTests(InventoryMyTablesFixture fixture)
  {
    this.fixture = fixture;
  }

  [Theory]
  [MemberData(nameof(MyTableData.Insert01)
      , MemberType = typeof(MyTableData))]
  public void Test01(int index, Item expected, string[] cmd)
  {
    fixture.AssertItemCount(fixture.Uow, index);
    fixture.RunCmd(fixture.Booter, cmd);
    fixture.AssertItemCount(fixture.Uow, index + 1);
    var actual = fixture.GetItem(fixture.Uow, index);
    fixture.AssertItem(expected, actual);
  }

  [Theory]
  [MemberData(nameof(MyTableData.Read01)
      , MemberType = typeof(MyTableData))]
  public void Test02(int index, string[] cmd, string expected)
  {
    fixture.RunCmd(fixture.Booter, cmd);
    var logger = fixture.Booter.GetLogger();
    logger
      .Should()
      .HaveMessage("{0} {1}")
      .Appearing().Once()
      .WithProperty("0")
      .WithValue("Read")
      .And
      .WithProperty("1")
      .WithValue("Item");
    var output = (IOutMock)fixture.Booter.GetOut();
    fixture.AssertItemCount(fixture.Uow, index + 1);
    var item = fixture.GetItem(fixture.Uow, index);
    var idStr = item.Id.ToString();
    expected = expected.Replace("{id}", idStr);
    var table = fixture.ItemTable;
    var idColumn = table.GetTable()[nameof(Item.Id)];
    expected = expected.Replace("{idcolleft}", new string(' ', idColumn.Left + 1));
    expected = expected.Replace("{idcolright}", new string(' ', idColumn.Right + 1));
    var outputText = output.OutText;
    dataUtil.PrintToFile(expected, outputText);
    Assert.Equal(expected, outputText);
  }
}
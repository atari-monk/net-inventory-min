using Inventory.Min.Cli.App.TestApi;
using Xunit;
using XUnit.Helper;
using data1 = Inventory.Min.Cli.App.Tests.ItemTests.TestUtil;
using data2 = Inventory.Min.Cli.App.Tests.ItemTests.DataUtil;

namespace Inventory.Min.Cli.App.Tests.ItemTests;

[Collection(DbTests)]
[TestCaseOrderer(OrdererTypeName, OrdererAssemblyName)]
public class UpdateParentTests
    : OrderTest
        , IClassFixture<InventoryFixture>
{
  private InventoryFixture fixture;

  public UpdateParentTests(InventoryFixture fixture)
  {
    this.fixture = fixture;
  }

  [Theory]
  [MemberData(nameof(UpdateParentData.Insert01), MemberType = typeof(UpdateParentData))]
  public void Test01(int index, string[] cmd)
  {
    fixture.AssertItemCount(fixture.Uow, index);
    fixture.RunCmd(fixture.Booter, cmd);
    fixture.AssertItemCount(fixture.Uow, index + 1);
  }

  [Theory]
  [MemberData(nameof(UpdateParentData.Update01), MemberType = typeof(UpdateParentData))]
  public void Test02(int index, string propName, string[] cmd)
  {
    Assert.True(index >= 0 && index < 2);
    fixture.AssertItemCount(fixture.Uow, 2);
    var itemDb = fixture.GetItem(fixture.Uow, 0);
    var itemDb2 = fixture.GetItem(fixture.Uow, 1);
    var command = new List<string>(cmd);
    data1.SetValue(command, "itemid", itemDb.Id.ToString());
    data1.SetValue(command, "parentid", itemDb2.Id.ToString());
    fixture.RunCmd(fixture.Booter, command.ToArray());
    fixture.AssertItemCount(fixture.Uow, 2);
    itemDb = fixture.GetItem(fixture.Uow, 0);
    var expected = data2.GetItem();
    expected.ParentId = itemDb2.Id;
    fixture.AssertItem(expected, itemDb, propName);
  }
}
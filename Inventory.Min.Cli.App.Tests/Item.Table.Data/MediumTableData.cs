using Inventory.Min.BetterTable;
using Inventory.Min.Data;
using dataUtil = Inventory.Min.Cli.App.Tests.ItemTests.DataUtil;

namespace Inventory.Min.Cli.App.Tests.ItemTests;

public class MediumTableData
    : MyTableData
{
  private const int CategoryId = 1;
  private const string CategoryName = "Container";
  private const int InitialCount = 9;
  private const int CurrentCount = 6;
  private const int Mass = 500;
  private const int StateId = 1;
  private const string StateName = "In storage";
  private const string ParentName = dataUtil.Name;

  public new static IEnumerable<object[]> Insert01 =>
      new List<object[]>
      {
            new object[]
            {
                0
                , dataUtil.GetItem()
                , dataUtil.GetInsCmd()
            }
      };

  public static IEnumerable<object[]> Insert02 =>
      new List<object[]>
      {
            new object[]
            {
                1
                , dataUtil.GetItem(
                    (item) => item.Description = dataUtil.Description
                    , (item) => item.CategoryId = CategoryId
                    , (item) => item.InitialCount = InitialCount
                    , (item) => item.CurrentCount = CurrentCount
                    , (item) => item.Mass = Mass
                    , (item) => item.StateId = StateId
                    )
                , dataUtil.GetInsCmd(
                    "-d", dataUtil.Description
                    , "-c", CategoryId.ToString()
                    , "-q", InitialCount.ToString()
                    , "--currentCount", CurrentCount.ToString()
                    , "--mass", Mass.ToString()
                    , "-g", StateId.ToString()
                    , "-f", "parentId")
            }
      };

  public new static IEnumerable<object[]> Read01 =>
      new List<object[]>
      {
            new object[]
            {
                1
                , dataUtil.GetReadCmd("-t", ItemTablesTest.MediumTest.ToString())
                ,    "┌──────────────┬─────────────────────┬───────────┬──────────────┬──────────────┬──────┬────────────┬──────────────┐\r\n│"
                +   GetHeader()
                +    "├──────────────┼─────────────────────┼───────────┼──────────────┼──────────────┼──────┼────────────┼──────────────┤\r\n│"
                +   GetRow1()
                +   GetRow2()
                +    "└──────────────┴─────────────────────┴───────────┴──────────────┴──────────────┴──────┴────────────┴──────────────┘\r\n"
            }
      };

  private static string GetHeader()
  {
    return
        $" \u001b[38;2;255;255;255m    {nameof(Item.Name)}    \u001b[0m │"
    + $" \u001b[38;2;255;255;255m    {nameof(Item.Description)}    [0m │"
    + $" \u001b[38;2;255;255;255m {nameof(Item.Category)}[0m │"
    + $" \u001b[38;2;255;255;255m{nameof(Item.InitialCount)}[0m │"
    + $" \u001b[38;2;255;255;255m{nameof(Item.CurrentCount)}[0m │"
    + $" \u001b[38;2;255;255;255m{nameof(Item.Mass)}[0m │"
    + $" \u001b[38;2;255;255;255m   {nameof(Item.State)}  [0m │"
    + $" \u001b[38;2;255;255;255m   {nameof(Item.Parent)}   [0m │"
    + "\r\n";
  }

  private static string GetRow1()
  {
    return
        $" \u001b[38;2;255;255;255m{dataUtil.Name}\u001b[0m │"
    + $" \u001b[38;2;255;255;255m                   [0m │"
    + $" \u001b[38;2;255;255;255m         [0m │"
    + $" \u001b[38;2;255;255;255m            [0m │"
    + $" \u001b[38;2;255;255;255m            [0m │"
    + $" \u001b[38;2;255;255;255m    [0m │"
    + $" \u001b[38;2;255;255;255m          [0m │"
    + $" \u001b[38;2;255;255;255m            [0m │"
    + "\r\n";
  }

  private static string GetRow2()
  {
    return
    $"│ \u001b[38;2;255;255;255m{dataUtil.Name}\u001b[0m │"
    + $" \u001b[38;2;255;255;255m{dataUtil.Description}[0m │"
    + $" \u001b[38;2;255;255;255m{CategoryName}[0m │"
    + $" \u001b[38;2;255;255;255m      {InitialCount}     [0m │"
    + $" \u001b[38;2;255;255;255m      {CurrentCount}     [0m │"
    + $" \u001b[38;2;255;255;255m {Mass}[0m │"
    + $" \u001b[38;2;255;255;255m{StateName}[0m │"
    + $" \u001b[38;2;255;255;255m{ParentName}[0m │"
    + "\r\n";
  }
}
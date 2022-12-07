namespace Aoc.Day07;

public class Folder
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Folder(Folder parent, string name, int size)
  {
    Parent  = parent;
    Name    = name;
    Size    = size;
    Items   = new List<Item>();
    Folders = new List<Folder>();
  }
  public Folder       Parent  { get; private set; }
  public string       Name    { get; private set; }
  public List<Item>   Items   { get; private set; }
  public List<Folder> Folders { get; private set; }
  public int          Size    { get; set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== PROPERTY HELPERS ==========================

  public int SetSize()
  {
    int i_size   = 0;
    foreach (Item i in this.Items)
    {
      i_size += i.Size;
    }

    int f_size = 0;
    foreach (Folder f in this.Folders)
    {
      f_size += f.SetSize();
    }

    this.Size = i_size + f_size;

    return this.Size;
  }

  // ========== COLLECTION HELPERS ========================

  public void AddFolder(Folder folder)
  {
    this.Folders.Add(folder);
  }

  public void AddItem(Item item)
  {
    this.Items.Add(item);
  }

  public Folder GetFolderByName(string name)
  {
    Folder? result = null;
    foreach (var f in this.Folders)
    {
      if (f.Name == name)
      {
        result = f;
      }
    }
    if (result == null)
    {
      throw new FolderNotFound($"Could not find folder {name} in parent folder {this.Name}.");
    }
    return result;
  }

  // ========== RECURSIVE HELPERS =========================

  public int AggregateSmallFolderSize(int sum, int limit)
  {
      if (this.Size <= limit)
      {
        sum += this.Size;
      }
      foreach (var f in this.Folders)
      {
        sum = f.AggregateSmallFolderSize(sum, limit);
      }
      return sum;
  }

  public int FindBestCandidateSize(int best, int goal)
  {
    if (this.Size >= goal && this.Size < best)
    {
      best = this.Size;
    }
    foreach (var f in this.Folders)
    {
      best = f.FindBestCandidateSize(best, goal);
    }
    return best;
  }
}

// --------------------------------------------------------
// Custom Exceptions
// --------------------------------------------------------

public class FolderNotFound : Exception
{
    public FolderNotFound(string message) : base(message)
    {
    }
}

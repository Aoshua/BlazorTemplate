﻿namespace Models.Common;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}

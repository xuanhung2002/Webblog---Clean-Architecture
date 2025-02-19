﻿using WebBlog.Domain.Entities;

namespace WebBlog.Domain.Entities
{
    public class RefreshToken : EntityAuditBase
    {
        public Guid AppUserId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }
        public string CreatedByIp { get; set; }
        public DateTimeOffset? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? RevokedByToken { get; set; }
        public string? RevokedReason { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => Revoked == null && !IsExpired;
        
    }
}

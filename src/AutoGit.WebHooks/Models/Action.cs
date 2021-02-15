using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace AutoGit.WebHooks.Models
{
    [TypeConverter(typeof(EnumMemberConverter<Action>))]
    public enum Action
    {
        Unknown,
        
        [EnumMember(Value = "created")]
        Created,
        
        [EnumMember(Value = "completed")]
        Completed,
        
        [EnumMember(Value = "rerequested")]
        ReRequested,
        
        [EnumMember(Value = "requested_action")]
        RequestedAction,
        
        [EnumMember(Value = "reopened_by_user")]
        ReOpenedByUser,
        
        [EnumMember(Value = "closed_by_user")]
        ClosedByUser,
        
        [EnumMember(Value = "fixed")]
        Fixed,
        
        [EnumMember(Value = "appeared_in_branch")]
        AppearedInBranch,
        
        [EnumMember(Value = "reopened")]
        Reopened,
        
        [EnumMember(Value = "deleted")]
        Deleted,
        
        [EnumMember(Value = "revoked")]
        Revoked,
        
        [EnumMember(Value = "suspend")]
        Suspend,
        
        [EnumMember(Value = "unsuspend")]
        Unsuspend,
        
        [EnumMember(Value = "new_permissions_accepted")]
        NewPermissionsAccepted,
        
        [EnumMember(Value = "added")]
        Added,
        
        [EnumMember(Value = "removed")]
        Removed,
        
        [EnumMember(Value = "edited")]
        Edited,
        
        [EnumMember(Value = "pinned")]
        Pinned,
        
        [EnumMember(Value = "unpinned")]
        Unpinned,
        
        [EnumMember(Value = "closed")]
        Closed,
        
        [EnumMember(Value = "assigned")]
        Assigned,
        
        [EnumMember(Value = "unassigned")]
        Unassigned,
        
        [EnumMember(Value = "labeled")]
        Labeled,
        
        [EnumMember(Value = "unlabeled")]
        Unlabeled,
        
        [EnumMember(Value = "locked")]
        Locked,
        
        [EnumMember(Value = "unlocked")]
        Unlocked,
        
        [EnumMember(Value = "transferred")]
        Transferred,
        
        [EnumMember(Value = "milestoned")]
        Milestoned,
        
        [EnumMember(Value = "demiledstoned")]
        Demilestoned,
        
        [EnumMember(Value = "purchased")]
        Purchased,
        
        [EnumMember(Value = "pending_change")]
        PendingChange,
        
        [EnumMember(Value = "pending_change_cancelled")]
        PendingChangeCancelled,
        
        [EnumMember(Value = "changed")]
        Changed,
        
        [EnumMember(Value = "cancelled")]
        Cancelled,
        
        [EnumMember(Value = "renamed")]
        Renamed,
        
        [EnumMember(Value = "member_added")]
        MemberAdded,
        
        [EnumMember(Value = "member_removed")]
        MemberRemoved,
        
        [EnumMember(Value = "member_invited")]
        MemberInvited,
        
        [EnumMember(Value = "blocked")]
        Blocked,
        
        [EnumMember(Value = "unblocked")]
        Unblocked,
        
        [EnumMember(Value = "published")]
        Published,
        
        [EnumMember(Value = "updated")]
        Updated,
        
        [EnumMember(Value = "moved")]
        Moved,
        
        [EnumMember(Value = "converted")]
        Converted,
        
        [EnumMember(Value = "review_requested")]
        ReviewRequested,
        
        [EnumMember(Value = "review_request_removed")]
        ReviewRequestRemoved,
        
        [EnumMember(Value = "ready_for_review")]
        ReadyForReview,
        
        [EnumMember(Value = "converted_to_draft")]
        ConvertedToDraft,
        
        [EnumMember(Value = "synchronize")]
        Synchronize,
        
        [EnumMember(Value = "auto_merge_enabled")]
        AutoMergeEnabled,
        
        [EnumMember(Value = "auto_merge_disabled")]
        AutoMergeDisabled,
        
        [EnumMember(Value = "submitted")]
        Submitted,
        
        [EnumMember(Value = "dismissed")]
        Dismissed,
        
        [EnumMember(Value = "unpublished")]
        Unpublished,
        
        [EnumMember(Value = "prereleased")]
        PreReleased,
        
        [EnumMember(Value = "released")]
        Released,
        
        [EnumMember(Value = "archived")]
        Archived,
        
        [EnumMember(Value = "unarchived")]
        Unarchived,
        
        [EnumMember(Value = "publicized")]
        Publicized,
        
        [EnumMember(Value = "privatized")]
        Privatized,
        
        [EnumMember(Value = "dismiss")]
        Dismiss,
        
        [EnumMember(Value = "resolve")]
        Resolve,
        
        [EnumMember(Value = "create")]
        Create,
        
        [EnumMember(Value = "resolved")]
        Resolved,
        
        [EnumMember(Value = "performed")]
        Performed,
        
        [EnumMember(Value = "tier_changed")]
        TierChanged,
        
        [EnumMember(Value = "pending_cancellation")]
        PendingCancellation,
        
        [EnumMember(Value = "pending_tier_change")]
        PendingTierChange,
        
        [EnumMember(Value = "added_to_repository")]
        AddedToRepository,
        
        [EnumMember(Value = "removed_from_repository")]
        RemovedFromRepository,
        
        [EnumMember(Value = "started")]
        Started
    }
    
    public class EnumMemberConverter<T> : EnumConverter 
    {
        public EnumMemberConverter(Type type) : base(type)
        {
            
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var type = typeof(T);

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute &&
                    value is string enumValue &&
                    attribute.Value == enumValue)
                {
                    return field.GetValue(null);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
USE [PROD_ISS_NAV]
GO

/****** Object:  View [dbo].[INV_DELIVERY_STATUS_BRANCH]    Script Date: 03/28/2014 11:21:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[INV_DELIVERY_STATUS_BRANCH]
AS
SELECT     COUNT(InvStatus) AS PROCEEDS, DELIVERED = 0, RETURNED = 0, DONE = 0, CONVERT(nvarchar(10), InvProceedsDate, 120) AS PROCEEDSDATE, 
                      CONVERT(nvarchar(10), InvDeliveredDate, 120) AS DELIVEREDDATE, CONVERT(nvarchar(10), InvReturnedDate, 120) AS RETURNEDDATE, CONVERT(nvarchar(10), 
                      InvDoneDate, 120) AS DONEDATE, InvBranch AS BRANCH
/*INTO #INV_DELIVERY_STATUS_AWAL */ FROM INV_DELIVERY
WHERE     Disabled = 0 AND InvStatus = 'PROCEEDS'
GROUP BY InvProceedsDate, InvDeliveredDate, InvReturnedDate, InvDoneDate, InvBranch
UNION
SELECT     PROCEEDS = 0, COUNT(InvStatus) AS DELIVERED, RETURNED = 0, DONE = 0, CONVERT(nvarchar(10), InvProceedsDate, 120) AS PROCEEDSDATE, 
                      CONVERT(nvarchar(10), InvDeliveredDate, 120) AS DELIVEREDDATE, CONVERT(nvarchar(10), InvReturnedDate, 120) AS RETURNEDDATE, CONVERT(nvarchar(10), 
                      InvDoneDate, 120) AS DONEDATE, InvBranch AS BRANCH
FROM         INV_DELIVERY
WHERE     Disabled = 0 AND InvStatus = 'DELIVERED'
GROUP BY InvProceedsDate, InvDeliveredDate, InvReturnedDate, InvDoneDate, InvBranch
UNION
SELECT     PROCEEDS = 0, DELIVERED = 0, COUNT(InvStatus) AS RETURNED, DONE = 0, CONVERT(nvarchar(10), InvProceedsDate, 120) AS PROCEEDSDATE, 
                      CONVERT(nvarchar(10), InvDeliveredDate, 120) AS DELIVEREDDATE, CONVERT(nvarchar(10), InvReturnedDate, 120) AS RETURNEDDATE, CONVERT(nvarchar(10), 
                      InvDoneDate, 120) AS DONEDATE, InvBranch AS BRANCH
FROM         INV_DELIVERY
WHERE     Disabled = 0 AND InvStatus = 'RETURNED'
GROUP BY InvProceedsDate, InvDeliveredDate, InvReturnedDate, InvDoneDate, InvBranch
UNION
SELECT     PROCEEDS = 0, DELIVERED = 0, RETURNED = 0, COUNT(InvStatus) AS DONE, CONVERT(nvarchar(10), InvProceedsDate, 120) AS PROCEEDSDATE, 
                      CONVERT(nvarchar(10), InvDeliveredDate, 120) AS DELIVEREDDATE, CONVERT(nvarchar(10), InvReturnedDate, 120) AS RETURNEDDATE, CONVERT(nvarchar(10), 
                      InvDoneDate, 120) AS DONEDATE, InvBranch AS BRANCH
FROM         INV_DELIVERY
WHERE     Disabled = 0 AND InvStatus = 'DONE'
GROUP BY InvProceedsDate, InvDeliveredDate, InvReturnedDate, InvDoneDate, InvBranch


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'INV_DELIVERY_STATUS_BRANCH'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'INV_DELIVERY_STATUS_BRANCH'
GO



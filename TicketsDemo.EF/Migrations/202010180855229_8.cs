namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceComponent", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.PriceComponent", "Ticket_ReservationId", "dbo.Ticket");
            DropIndex("dbo.PriceComponent", new[] { "TicketId" });
            DropPrimaryKey("dbo.Ticket");
            AddColumn("dbo.PriceComponent", "Ticket_ReservationId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Ticket", "ReservationId");
            CreateIndex("dbo.Run", "TrainId");
            CreateIndex("dbo.Reservation", "PlaceInRunId");
            CreateIndex("dbo.Ticket", "ReservationId");
            CreateIndex("dbo.PriceComponent", "Ticket_ReservationId");
            AddForeignKey("dbo.Reservation", "PlaceInRunId", "dbo.PlaceInRun", "Id");
            AddForeignKey("dbo.Ticket", "ReservationId", "dbo.Reservation", "Id");
            AddForeignKey("dbo.Run", "TrainId", "dbo.Train", "Id");
            AddForeignKey("dbo.PriceComponent", "Ticket_ReservationId", "dbo.Ticket", "ReservationId");
            DropColumn("dbo.Ticket", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ticket", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PriceComponent", "Ticket_ReservationId", "dbo.Ticket");
            DropForeignKey("dbo.Run", "TrainId", "dbo.Train");
            DropForeignKey("dbo.Ticket", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.Reservation", "PlaceInRunId", "dbo.PlaceInRun");
            DropIndex("dbo.PriceComponent", new[] { "Ticket_ReservationId" });
            DropIndex("dbo.Ticket", new[] { "ReservationId" });
            DropIndex("dbo.Reservation", new[] { "PlaceInRunId" });
            DropIndex("dbo.Run", new[] { "TrainId" });
            DropPrimaryKey("dbo.Ticket");
            DropColumn("dbo.PriceComponent", "Ticket_ReservationId");
            AddPrimaryKey("dbo.Ticket", "Id");
            CreateIndex("dbo.PriceComponent", "TicketId");
            AddForeignKey("dbo.PriceComponent", "Ticket_ReservationId", "dbo.Ticket", "ReservationId");
            AddForeignKey("dbo.PriceComponent", "TicketId", "dbo.Ticket", "Id", cascadeDelete: true);
        }
    }
}

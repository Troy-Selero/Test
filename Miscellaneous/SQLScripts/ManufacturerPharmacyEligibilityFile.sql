use [Selena]

select * from ManufacturerPharmacyEligibilityBatch with (nolock)

select *
from ManufacturerPharmacyEligibilityFile with (nolock)
where Manufacturer='abbvie'
and PharmacyID=612

select *
from ManufacturerPharmacyEligibilityFileStatus with (nolock)
--where ManufacturerPharmacyEligibilityFileID=29

/*
select Manufacturer, COUNT(*)
from ManufacturerPharmacyEligibilityFile
group by Manufacturer

select
	mpef.ManufacturerPharmacyEligibilityFileUID, mpef.[Version], mpef.Manufacturer, mpef.PharmacyID, mpef.PharmacyName, mpef.RetailStatus
	,pefs.ManufacturerPharmacyEligibilityFileID, pefs.CurrentVersion, pefs.RetailStatus
from ManufacturerPharmacyEligibilityFile mpef with (nolock)
	inner join ManufacturerPharmacyEligibilityFileStatus pefs with (nolock) on pefs.ManufacturerPharmacyEligibilityFileID=mpef.ManufacturerPharmacyEligibilityFileID and pefs.CurrentVersion=mpef.[Version]
where
	mpef.Manufacturer='abbvie'

select *
from vew_ManufacturerPharmacyEligibilityFileCurrent
where Manufacturer='abbvie'
and PharmacyID=612
*/

/*
truncate table ManufacturerPharmacyEligibilityBatch
truncate table ManufacturerPharmacyEligibilityFile
truncate table ManufacturerPharmacyEligibilityFileStatus
*/
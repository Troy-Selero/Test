use [Selero]

select * from PharmacyMfrEligibilityBatch with (nolock)

select *
from PharmacyMfrEligibilityFile with (nolock)
where
	Manufacturer='abbvie'
	and PharmacyID=612

select * from PharmacyMfrEligibilityFileStatus with (nolock)

/*
select Manufacturer, COUNT(*)
from PharmacyMfrEligibilityFile
group by Manufacturer

select *
from vew_PharmacyMfrEligibilityFileCurrent
where Manufacturer='abbvie'
and PharmacyID=612

-- truncate table PharmacyMfrEligibilityBatch
-- truncate table PharmacyMfrEligibilityFile
-- truncate table PharmacyMfrEligibilityFileStatus
*/
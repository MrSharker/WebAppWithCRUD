# WebAppWithCRUD

This is the test project for Pulseem company.

Here realize the CRUD for the Client with rules:
- Email is unique and can exists only once – It’s a required field
- Cellphone can exists once or multiple times in case they have different emails.
- Cellphone can start with 05 (len of 10 – local number) or with 9725 (len of 12 – International
number) – it means that 972526292959=0526292959
- EmailStatus/SmsStatus can have only 2 options – Removed/Active
- If you change the SmsStatus – all the relevant cellphone will change as well (means, if I change
0526292959 to removed, all other cellphone with 0526292959 and 972526292959 will change as
well to removed)
- When you add Records - Both Status fields are hidden (only when insert, when update those fields
are shown)
New Email and New Cellphone will get insert with Active Status
- If you add a cellphone that exists in the DB, it should inherit its sms status

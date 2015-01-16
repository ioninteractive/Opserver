﻿using System.Linq;
using System.Web.Mvc;
using StackExchange.Opserver.Data.PagerDuty;
using StackExchange.Opserver.Helpers;
using StackExchange.Opserver.Models;
using StackExchange.Opserver.Views.PagerDuty;

namespace StackExchange.Opserver.Controllers
{
    [OnlyAllow(Roles.PagerDuty)]
    public partial class PagerDutyController : StatusController
    {
        protected override ISecurableSection SettingsSection
        {
            get { return Current.Settings.PagerDuty; }
        }
        protected override string TopTab
        {
            get { return TopTabs.BuiltIn.PagerDuty; }
        }
        
        [Route("pagerduty")]
        public ActionResult PagerDutyDashboard()
        {
            var i = PagerDutyApi.Instance;
            var vd = new PagerDutyModel
            {
                AllOnCall = i.AllUsers.SafeData(true),
                OnCallToShow = i.Settings.OnCalllToShow,
                AllIncidents = i.Incidents.SafeData(true)
            };
            return View("PagerDuty", vd);
        }

        [Route("pagerduty/incident/detail/{id}")]
        public ActionResult PagerDutyIncidentDetail(int id)
        {
            var incident = PagerDutyApi.Instance.Incidents.Data.First(i => i.Number == id);
            return View("PagerDuty.Incident", incident);
        }

        [Route("pagerduty/escalation/full")]
        public ActionResult PagerDutyFullEscalation()
        {
            return View("PagerDuty.EscFull", PagerDutyApi.Instance.AllUsers.SafeData(true));
        }
    }
}
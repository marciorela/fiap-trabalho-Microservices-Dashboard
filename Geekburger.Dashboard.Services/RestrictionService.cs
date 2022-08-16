using Geekburger.Dashboard.Contract.DTOs;
using Geekburger.Dashboard.Data;
using Geekburger.Dashboard.Domain;
using Geekburger.Dashboard.Domain.Entities;
using Geekburger.Dashboard.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Services
{
    public class RestrictionService
    {
        private readonly RestrictionRepository _restrictionRepository;

        public RestrictionService(RestrictionRepository restrictionRepository)
        {
            _restrictionRepository = restrictionRepository;
        }

        public async Task Add(UserWithLessOffer _userWithLessOffer)
        {
            var restrictions = _userWithLessOffer.Restrictions.Select(x => new Restriction()
            {
                UserId = _userWithLessOffer.UserId,
                Name = x
            });

            await _restrictionRepository.AddRange(restrictions);
        }

        public async Task<List<UsersWithLessOfferResponse>> GetAll()
        {
            var results = new List<UsersWithLessOfferResponse>();
            var grouped = new List<UserWithLessOffer>();

            var list = await _restrictionRepository.GetAll();
            foreach (var item in list)
            {
                var group = grouped.Where(x => x.UserId == item.UserId).FirstOrDefault();
                if (group == null)
                {
                    group = new UserWithLessOffer();
                    grouped.Add(group);
                }
                group.UserId = item.UserId;
                group.Restrictions.Append(item.Name);
            }

            foreach (var item in grouped)
            {
                if (item.Restrictions.Count() < 4)
                {
                    var strRestriction = string.Join(",", item.Restrictions);
                    var result = results.Where(x => strRestriction == x.Restrictions).FirstOrDefault();
                    if (result == null)
                    {
                        result = new UsersWithLessOfferResponse();
                        results.Add(result);
                    }
                    result.Users++;
                    result.Restrictions = strRestriction;
                }
            }

            return results;
        }

    }
}

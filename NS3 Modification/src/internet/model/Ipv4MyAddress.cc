#include "Ipv4MyAddress.h"

namespace ns3
{
    TypeId
    Ipv4MyAddress::GetTypeId()
    {
        static TypeId tid = TypeId("ns3::Ipv4MyAddress")
          .SetParent<Object>()
          .SetGroupName("Internet")
          .AddConstructor<Ipv4MyAddress>()
        ;
        
        return tid;
    }
    
    Ipv4MyAddress::Ipv4MyAddress()
    {
        ipaddr = Ipv4Address();
    }
    
    Ipv4MyAddress::Ipv4MyAddress(const Ipv4MyAddress& orig)
    {
        ipaddr = Ipv4Address(orig.getAddr());
    }
    
    void
    Ipv4MyAddress::setAddr(const Ipv4Address ipa)
    {
        ipaddr = ipa;
    }
    
    Ipv4Address
    Ipv4MyAddress::getAddr() const
    {
        return ipaddr;
    }
    
    Ipv4MyAddress::~Ipv4MyAddress()
    {
        
    }
}
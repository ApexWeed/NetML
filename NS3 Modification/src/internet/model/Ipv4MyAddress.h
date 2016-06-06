/* 
 * File:   Ipv4MyAddress.h
 * Author: addie
 *
 * Created on 1 February 2012, 10:07 AM
 */

#ifndef IPV4MYADDRESS_H
#define	IPV4MYADDRESS_H

#include <stdint.h>
#include "ns3/object.h"
#include "ns3/socket.h"
#include "ns3/callback.h"
#include "ns3/ipv4-address.h"
#include "ipv4-route.h"
#include "ipv4-interface-address.h"

namespace ns3 {


class Ipv4MyAddress : public Object {
public:
  static TypeId GetTypeId (void);
    Ipv4Address ipaddr;
    Ipv4MyAddress();
    Ipv4MyAddress(const Ipv4MyAddress& orig);
    void setAddr(const Ipv4Address ipa);
    Ipv4Address getAddr() const;
    virtual ~Ipv4MyAddress();
private:

};

} // namespace ns3 

#endif	/* IPV4MYADDRESS_H */

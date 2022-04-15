# credit-suisse-assessment
mini-project as part of CS candidate appraisal

## notes ##

As per the specification I'm keeping this nice &amp; simple. Thus;

1. The PriceStream class does not extend System.IO.Stream but adheres to the general computer science definition of a Stream; a sequence of data elements. Also any IDisposable implementation would be unnecessary
2. The OrderComponent class does not extend System.ComponentModel.Component or implement System.ComponentModel.IComponent
3. Processing is synchonous and sequential thus negating need for eventing, threading, locking, concurrency collections etc. To meet the "only one component instance should make the purchase" requirement a first-come-first-served approach is used
4. I assume that data volumes are small and so I've deliberately kept looping etc simple. i.e. Linq could likely be utilized to increase performance
5. I didn't add a mirror unit-tests project as that'd definitely feel like "doing too much" but somewhat contravenes the "write as part of a production grade system" requirement

xml comments are added as the specification mentioned &quot;write the code as you would write as part of a production grade system&quot;. However I haven&#39;t created project sub-folders and associated namespaces as there's too few classes to warrent this.

no restrictive class and method access has been applied. i.e. declarations are public instead of internal etc

I&#39;ve assumed we only ever make 1 purchase of a particular product. So many orders for a product will only generate 1 purchase

Where applicable I used ReadOnlyDictionary as it does not add complexity and neatly indentifies where source values are fixed

